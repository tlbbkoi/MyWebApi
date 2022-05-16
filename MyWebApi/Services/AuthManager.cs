using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Controllers;
using MyWebApi.Data;
using MyWebApi.Mail;
using MyWebApi.Models;
using MyWebApi.Properties;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyWebApi.Services
    {
        public class AuthManager : IAuthManager
        {
            private readonly UserManager<ApiUser> _userManager;
            private readonly IConfiguration _configuration;
            private ApiUser _user;
            private readonly ILogger<AccountController> _logger;
            private readonly IMapper _mapper;
            private readonly ISendMailService _sendMailService;
            private readonly IHttpContextAccessor _httpContextAccessor;
            


        public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration,
                            ILogger<AccountController> logger, IMapper mapper, 
                            IHttpContextAccessor httpContextAccessor, ISendMailService sendMailService)
            {
                _userManager = userManager;
                _configuration = configuration;
                _logger = logger;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
                _sendMailService = sendMailService;
            }
            public async Task<string> CreateToken()
            {
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims();
                var token = GenerateTokenOptions(signingCredentials, claims);
                var nToken = new JwtSecurityTokenHandler().WriteToken(token);
                IsValidToken(nToken);
                return nToken;
            }

            private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
            {
                var jwtSettings = _configuration.GetSection("Jwt");

                var expiration = DateTime.Now.AddMinutes(
                    int.Parse(jwtSettings.GetSection("lifetime").Value));

                var token = new JwtSecurityToken(
                    issuer: jwtSettings.GetSection("Issuer").Value,
                    audience: jwtSettings.GetSection("Audience").Value,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signingCredentials
                    );
                return token;
            }

            private async Task<List<Claim>> GetClaims()
            {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.UserName)
                };

                var roles = await _userManager.GetRolesAsync(_user);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                return claims;
            }

            private SigningCredentials GetSigningCredentials()
            {
                var jwtSettings = _configuration.GetSection("Jwt");
                var key = jwtSettings.GetSection("Key").Value;
                var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            }

            public async Task<string> Login(LoginUserDTO userDTO)
            {
                _user = await _userManager.FindByNameAsync(userDTO.Email);
                var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password));
                if(_user != null && result)
                {
                    return await CreateToken();
                }
                throw new BusinessException(Resource.LOGIN_FAIL);
            
            }

            public bool IsValidToken(string token)
            {
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var jwtSettings = _configuration.GetSection("Jwt");
                try
                {
                    jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("Key").Value)),

                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.GetSection("Audience").Value,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    }, out var _);
                    return true;
                }
                catch (Exception err)
                {
                    return false;
                }
            }

        public async Task<bool> Register(UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attempl for {userDTO.Email}");


            var user = _mapper.Map<ApiUser>(userDTO);
            user.UserName = userDTO.Email;
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded)
            {
                List<string> error = new List<string>();
                foreach (var err in result.Errors)
                {
                    error.Add(err.Description);
                }

                throw new BusinessException(error[0]);
            }

            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
            string url = $"{_configuration["AppUrl"]}/api/account/confirmedemail?id={user.Id}&token={validEmailToken}";
            
            await _sendMailService.SendEmailAsync(userDTO.Email,Resource.VERIFICATION_REQUIRED , $"{url}");
            

            await _userManager.AddToRolesAsync(user, userDTO.Roles);
 
            return true;
        }

        public async Task<string> Logout()
        {
            var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;

            //Gets list of claims.
            IEnumerable<Claim> claims = identity.Claims;

            var usernameClaim = claims
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();

            var user = await _userManager.FindByNameAsync(usernameClaim.Value);
            var result = await _userManager.RemoveAuthenticationTokenAsync(user, "Web", "Access");
            if (result.Succeeded)
            {
                return Resource.LOGOUT_SUCCESS;
            }
            throw new BusinessException(Resource.LOGOUT_FAIL);
        }

        public async Task<string> ConfimedEmail(string userId, string token)
        {
            List<string> error = new List<string>();
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                throw new BusinessException(error[0]);
            }
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);
            if (result.Succeeded)
            {
                return Resource.COMFIRMED_SUCCESS;
            }
            foreach (var e in result.Errors)
            {
                error.Add(e.Description);
            }
            throw new BusinessException(error[0]);
        }

        public async Task<string> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if(user == null )
            {
                return Resource.FORGOT_PASSWORD_FAIL;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);


            await _sendMailService.SendEmailAsync(email, Resource.VERIFICATION_REQUIRED, $"{validToken}");

            return Resource.FORGOT_PASSWORD_SUCCESS;

        }

        public async Task<string> ResetPassword(string token, ResetPassword resetPassword)
        {   
            if(token == null)
            {
                return Resource.NOT_TOKEN;
            }
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if(user == null)
            {
                return Resource.NOT_ACCOUNT;
            }
            if(resetPassword.Password != resetPassword.ConfirmPassword)
            {
                return Resource.PASSWORD_NOT_MATCH;
            }
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, resetPassword.Password);
            if (!result.Succeeded)
            {
                return Resource.RESETPASSWORD_FAIL;
            }
            return Resource.RESETPASSWORD_SUCCESS;

        }
    }
    }

