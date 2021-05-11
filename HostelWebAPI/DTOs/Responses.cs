using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI
{
    public partial class MessageResponse
    {
        public MessageResponse(object errors, object messages)
        {
            Errors = errors;
            Messages = messages;
        }
        public object Errors { get; set; }
        public object Messages { get; set; }
    }

    public partial class TokenResponse
    {
        public TokenResponse(string userId, string token, string name, string email)
        {
            UserId = userId;
            Token = token;
            Name = name;
            Email = email;
        }

        public TokenResponse(string userId, string token)
        {
            UserId = userId;
            Token = token;
        }

        public string UserId { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; }
    }

    public class ReservedDate
    {
        public ReservedDate(Models.ReservationHistory res)
        {
            FromDate = res.FromDate.ToUniversalTime();
            ToDate = res.ToDate.ToUniversalTime();
        }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class ImageResponse
    {
        public ImageResponse(Models.Image i)
        {
            Id = i.ImageId;
            Url = i.Url;
            Alt = i.Alt;
            DeleteHash = i.DeleteHash;
        }

        [JsonConstructor]
        public ImageResponse(string url, string? alt, string? deleteHash)
        {
            Url = url;
            Alt = alt;
            DeleteHash = deleteHash;
        }
        public string Id { get; set; }
        public string Url { get; set; }
        public string? Alt { get; set; }
        public string? DeleteHash { get; set; }
    }

    /// <summary>
    /// Full response for property's info
    /// </summary>
    public class PropertyResponse : PropertyViewResponse
    {

        public PropertyResponse(Models.Property p, IEnumerable<ReservedDate> reservedDates) : base(p)
        {
            if (p.Images != null)
                Images = p.Images.Select(i => new ImageResponse(i)).ToList();
            Introduction = p.Introduction;
            MaxGuest = p.MaxPeople;
            ReservedDates = reservedDates;
            ServiceFee = p.ServiceFee;
            CleaningFee = p.CleaningFee;
        }
        public int MaxGuest { get; set; }
        public IEnumerable<ImageResponse>? Images { get; set; }
        public string Introduction { get; set; }
        public bool? Liked { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal CleaningFee { get; set; }

        public IEnumerable<Models.Comment>? Comments { get; set; }
        public IEnumerable<ReservedDate>? ReservedDates { get; set; }
        public IEnumerable<DateTime>? DaysOff { get; set; }
    }

    /// <summary>
    /// Response for property's thumbnail view
    /// </summary>
    public class PropertyViewResponse
    {
        public PropertyViewResponse(Models.Property p)
        {
            Id = p.PropertyId;
            Name = p.Name;
            if (p.ThumbnailUrl == null && p.Images.Count != 0) ThumbnailUrl = p.Images.ElementAt(0).Url;
            else ThumbnailUrl = p.ThumbnailUrl;
            Description = p.Description;
            Location = p.PropertyAddress != null ? p.PropertyAddress.City.Name : null;
            TotalReview = p.TotalReview;
            TotalStar = p.TotalStar;
            FormattedPrice = p.PricePerNight;
            Services = getServices(p.PropertyService);
        }

        protected IEnumerable<string> getServices(Models.PropertyService s)
        {
            List<string> services = new List<string>();
            if (s != null)
            {
                if (s.Breakfast == true) services.Add(nameof(s.Breakfast));
                if (s.Kitchen == true) services.Add(nameof(s.Kitchen));
                if (s.PetAllowed == true) services.Add("Pet");
                if (s.Wifi == true) services.Add(nameof(s.Wifi));
            }

            return services;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int TotalReview { get; set; }
        public int TotalStar { get; set; }
        public decimal FormattedPrice { get; set; }
        public IEnumerable<string> Services { get; set; }
    }

    public class ServiceRequest
    {
        public bool Wifi { get; set; }
        public bool Kitchen { get; set; }
        public bool Breakfast { get; set; }
        public bool Pet { get; set; }
        public bool Parking { get; set; }
    }

}
