using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.Models
{
    public class Service
    {
        public Service()
        {
            PropertyServices = new HashSet<PropertyService>();
        }

        [Key]
        [Required]
        [MaxLength(50)]
        public string ServiceId { get; set; }

        [MaxLength(50)]
        public string ServiceName { get; set; }

        public virtual ICollection<PropertyService> PropertyServices { get; set; }
    }
}
