using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Services
{
    using global::MyWebApi.Data;
    using global::MyWebApi.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    namespace MyWebApi.Services
    {
        public class AuthManager : IAuthManager
        {
            private readonly UserManager<ApiUser> _userManager;
            private readonly IConfiguration _configuration;
            private ApiUser _user;

            public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
            {
                _userManager = userManager;
                _configuration = configuration;
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

            public async Task<bool> ValidateUser(LoginUserDTO userDTO)
            {
                _user = await _userManager.FindByNameAsync(userDTO.Email);
                return (_user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password));
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
        }
    }

}
