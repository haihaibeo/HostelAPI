using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelWebAPI.Models
{
    public partial class Review
    {
        public Review()
        {

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

        [ForeignKey(nameof(PropId))]
        public virtual Property Property { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

    }
}