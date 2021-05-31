using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class Property
    {
        public Property()
        {
            Comments = new HashSet<Comment>();
            Images = new HashSet<Image>();
            Reviews = new HashSet<Review>();
            ReservationHistories = new HashSet<ReservationHistory>();
            UserPropertyLikes = new HashSet<UserPropertyLike>();
        }

        [Key]
        [Column("PropertyID")]
        [StringLength(50)]
        public string PropertyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [MaxLength(2000)]
        public string Introduction { get; set; }

        [DefaultValue(1)]
        public int MaxPeople { get; set; }

        [StringLength(100)]
        public string ThumbnailUrl { get; set; }

        [Required]
        [Column("PropertyTypeID")]
        [StringLength(50)]
        public string PropertyTypeId { get; set; }

        // [Required]
        [StringLength(50)]
        public string PropertyStatusId { get; set; }

        [DefaultValue(0)]
        public int TotalReview { get; set; }

        [DefaultValue(0)]
        public int TotalStar { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TimeCreated { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TimeUpdated { get; set; }

        [Required]
        [Column("OwnerID")]
        public string OwnerId { get; set; }

        [Column(TypeName = "money")]
        public decimal PricePerNight { get; set; }

        [Column(TypeName = "money")]
        public decimal ServiceFee { get; set; }

        [Column(TypeName = "money")]
        public decimal CleaningFee { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual Owner Owner { get; set; }

        [ForeignKey(nameof(PropertyTypeId))]
        public virtual PropertyType PropertyType { get; set; }

        public virtual PropertyAddress PropertyAddress { get; set; }

        [ForeignKey(nameof(PropertyStatusId))]
        public virtual PropertyStatus PropertyStatus { get; set; }

        public virtual PropertyService PropertyService { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ReservationHistory> ReservationHistories { get; set; }

        public virtual ICollection<UserPropertyLike> UserPropertyLikes { get; set; }
    }
}
