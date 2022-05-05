using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{   
    public class LoginUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserDTO : LoginUserDTO
    {
        public string FistName { get; set; }
        public string LastName { get; set; }

        public string phoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }
        
    }

    public class ResetPassword
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
