using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{   
    public class LoginUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Your Password is limit {2} to {1} character", MinimumLength = 6)]
        public string Password { get; set; }
    }
    public class UserDTO : LoginUserDTO
    {
        public string FistName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }
        
    }
}
