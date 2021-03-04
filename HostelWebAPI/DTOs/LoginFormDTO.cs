using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DTOs
{
    public class LoginFormDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RegisterDTO
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class ReturnTokenDTO
    {
        public ReturnTokenDTO(string userId, string token, string userName)
        {
            UserId = userId;
            Token = token;
            UserName = userName;
        }

        public ReturnTokenDTO(string userId, string token)
        {
            UserId = userId;
            Token = token;
        }

        public string UserId { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
    }
}
