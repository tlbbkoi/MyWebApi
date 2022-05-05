using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Services
{
    public interface IAuthManager
    {
        Task<string> Login(LoginUserDTO userDTO);
        Task<string> CreateToken();
        Task<bool> Register(UserDTO userDTO);
        Task<string> Logout();
        Task<string> ConfimedEmail(string userId, string token);
        Task<string> ForgotPassword(string email);
        Task<string> ResetPassword(string token, ResetPassword resetPassword);
        
    }
}
