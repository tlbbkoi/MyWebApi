using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Data;
using MyWebApi.Models;
using MyWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountController(UserManager<ApiUser> userManager, ILogger<AccountController> logger, IMapper mapper, IAuthManager authManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attempl for {userDTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);
                if (!result.Succeeded)
                {   
                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }

                    return BadRequest(ModelState);
                }
                 
                await _userManager.AddToRolesAsync(user, userDTO.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in the {nameof(Register)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");

            }
        }

       [HttpPost]
       [Route("login")]
         public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
         {
             _logger.LogInformation($"Login Attempl for {userDTO.Email}");
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
             try
             {
                 if(!await _authManager.ValidateUser(userDTO))
                {
                    return Unauthorized();
                }
                  return Accepted(new { Token = await _authManager.CreateToken() });
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex, $"Error in the {nameof(Login)}");
                 return StatusCode(500, "Internal Server Error. Please Try Again Later.");

             }

         }
        



    }
}
