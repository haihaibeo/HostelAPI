using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class Property
    {
        public Property()
        {
            Comment = new HashSet<Comment>();
            Image = new HashSet<Image>();
            ReservationHistory = new HashSet<ReservationHistory>();
            UserPropertyLike = new HashSet<UserPropertyLike>();
        }

        public string PropertyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Introduction { get; set; }
        public string PropertyTypeId { get; set; }
        public double Rating { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public string OwnerId { get; set; }
        public decimal PricePerNight { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual PropertyAddress PropertyAddress { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<ReservationHistory> ReservationHistory { get; set; }
        public virtual ICollection<UserPropertyLike> UserPropertyLike { get; set; }
    }
}
