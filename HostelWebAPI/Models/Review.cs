using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelWebAPI.Models
{
    public partial class Review
    {
        public Review()
        {

        }

        public Review(ReviewRequest req)
        {
            this.PropId = req.PropertyId;
            this.ReservationId = req.ReservationId;
            this.ReviewComment = req.ReviewComment;
            this.Star = req.StarCount;
            this.ReviewId = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        [MaxLength(50)]
        public string ReviewId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PropId { get; set; }

        [MaxLength(50)]
        public string ReservationId { get; set; }

        [Required]
        public int Star { get; set; }

        [MaxLength(1000)]
        public string ReviewComment { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TimeCreated { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TimeUpdated { get; set; }

        [ForeignKey(nameof(PropId))]
        public virtual Property Property { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

    }
}