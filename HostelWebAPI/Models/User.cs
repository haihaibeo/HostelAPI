using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            UserPropertyLikes = new HashSet<UserPropertyLike>();
            ReservationHistories = new HashSet<ReservationHistory>();
        }

        public override string Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public override string PhoneNumber { get; set; }
        public DateTime TimeCreated { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<UserPropertyLike> UserPropertyLikes { get; set; }

        public virtual ICollection<ReservationHistory> ReservationHistories { get; set; }
    }
}
