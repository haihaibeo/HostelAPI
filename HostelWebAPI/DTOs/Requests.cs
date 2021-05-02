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

    public class ReservationRequest
    {
        public string PropertyId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int AdultNum { get; set; }
        public int ChildrenNum { get; set; }
        public int InfantNum { get; set; }
    }

    public class PublishPropertyRequest
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Property's name exceeded in length")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Property's description exceeded in length")]
        public string Description { get; set; }

        [MaxLength(450, ErrorMessage = "Property's introduction exceeded in length")]
        public string Introduction { get; set; }

        [Required]
        public string CityId { get; set; }

        public string PropTypeId { get; set; }

        public int MaxGuest { get; set; }

        public string AddressDesc { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public List<ImageResponse> Images { get; set; }

        [Required]
        public ServiceRequest Services { get; set; }

        [Required]
        public int RefundPercent { get; set; }

        [Required]
        public decimal BasePrice { get; set; }
        public decimal ServiceFee { get; set; } = 0;
        public decimal CleaningFee { get; set; } = 0;
    }

}
