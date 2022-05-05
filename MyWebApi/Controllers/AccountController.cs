    using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Data;
using MyWebApi.Models;
using MyWebApi.Properties;
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
        
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var resuft = await _authManager.Register(userDTO);
            return Ok(new Repsonse(Resource.REGISTER_SUCCESS));
        }

       
       [HttpPost]
       [Route("login")]
         public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
         {
            var result = await _authManager.Login(userDTO);
            return Ok(new Repsonse(Resource.LOGIN_SUCCESS, null, new { Token = result }));

         }

       [HttpGet]
       [Route("logout")]

       public async Task<IActionResult> Logout()
        {
            var result = await _authManager.Logout();
            return Ok(new Repsonse(result));
        }

        [HttpGet]
        [Route("confirmedemail")]
        public async Task<IActionResult> ConfirmedEmail(string id, string token)
        {
            var result = await _authManager.ConfimedEmail(id, token);
            return Ok(new Repsonse(result));
        }

        [HttpPost]
        [Route("forgotpassword")]
        public async Task<IActionResult> ForgotPassWord(string email)
        {
            var result = await _authManager.ForgotPassword(email);
            return Ok(new Repsonse(result));

        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword(string token, [FromBody] ResetPassword resetPassword)
        {
            var result = await _authManager.ResetPassword(token, resetPassword);
            return Ok(new Repsonse(result));
        }
    }
}
