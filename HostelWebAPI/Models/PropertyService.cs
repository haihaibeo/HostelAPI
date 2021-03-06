using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.Models
{
    public class PropertyService
    {
        public PropertyService()
        {

        }

        [Key]
        [Required]
        [StringLength(50)]
        public string PropertyServiceId { get; set; }

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
