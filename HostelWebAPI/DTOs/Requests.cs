using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI
{
    public partial class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public partial class RegisterRequest
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

    public partial class RegisterPropertyRequest
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public string PropertyTypeId { get; set; }
        public IEnumerable<string> Images { get; set; }
        public string PricePerNight { get; set; }
        public string CityId { get; set; }
        public IEnumerable<string> ServiceIds { get; set; }
    }

    public partial class PropertyResponseViewResponse
    {

    }
}
