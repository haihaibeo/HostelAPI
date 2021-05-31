using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HostelWebAPI.Models
{
    public partial class PropertyStatus
    {
        public PropertyStatus()
        {
            Properties = new HashSet<Property>();
        }

        [Key]
        [MaxLength(50)]
        [Required]
        public string PropertyStatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}