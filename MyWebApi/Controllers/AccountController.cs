using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Data;
using MyWebApi.Models;
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

        public AccountController(UserManager<ApiUser> userManager, ILogger<AccountController> logger, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
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
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {   
                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                    return BadRequest("User Registraion Attempl Failed");
                }
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in the {nameof(Register)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");

            }
        }

        /* [HttpPost]
         [Route("login")]
         public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
         {
             _logger.LogInformation($"Login Attempl for {loginUserDTO.Email}");
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
             try
             {
                 var result = await _signInManager.PasswordSignInAsync(loginUserDTO.Email, loginUserDTO.Password,false,false);

                 if (!result.Succeeded)
                 {
                     return BadRequest("User Login Attempl Failed");
                 }
                 return Accepted();
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex, $"Error in the {nameof(Login)}");
                 return StatusCode(500, "Internal Server Error. Please Try Again Later.");

             }

         }
        */



    }
}
