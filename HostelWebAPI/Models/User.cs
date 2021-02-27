using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            ReservationHistory = new HashSet<ReservationHistory>();
            UserPropertyLike = new HashSet<UserPropertyLike>();
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime TimeCreated { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<ReservationHistory> ReservationHistory { get; set; }
        public virtual ICollection<UserPropertyLike> UserPropertyLike { get; set; }
    }
}
