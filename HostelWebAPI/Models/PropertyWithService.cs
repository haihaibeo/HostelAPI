using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelWebAPI.Models
{
    public partial class PropertyWithService
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PropertyId { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; }
    }
}