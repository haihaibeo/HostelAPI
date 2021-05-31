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

        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
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
        public int GuestNum { get; set; }
        public int ChildrenNum { get; set; }
        public int? InfantNum { get; set; }
    }
    public class PropertyQueryRequest
    {
#nullable enable
        public string? City { get; set; }
        public string? TypeId { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public int? GuestNum { get; set; }
        public int? ChildrenNum { get; set; }
#nullable disable
    }

    public class ImageRequest
    {
        public string Url { get; set; }
        public string? Alt { get; set; }
        public string? DeleteHash { get; set; }
    }

    public class ReviewRequest
    {
        public string PropertyId { get; set; }
        public string? ReservationId { get; set; }

        [Range(1, 5, ErrorMessage = "Star count needs to be from 1 to 5")]
        public int StarCount { get; set; }
        public string? ReviewComment { get; set; }
    }

    public class ValidationRequest
    {
        [Required]
        public string PropId { get; set; }
        [Required]
        public string PropStatusId { get; set; }
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

        public string CountryId { get; set; }
        public string PropTypeId { get; set; }

        public int MaxGuest { get; set; }

        public string AddressDesc { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public List<ImageRequest> Images { get; set; }

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
