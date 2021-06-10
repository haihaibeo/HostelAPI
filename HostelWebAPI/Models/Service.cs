using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HostelWebAPI.Models
{
    public partial class Service
    {
        public Service()
        {
            PropertyWithServices = new HashSet<PropertyWithService>();
        }

        [Key]
        [StringLength(50)]
        [Required]
        public string ServiceId { get; set; }

        [StringLength(50)]
        [Required]
        public string ServiceName { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<PropertyWithService> PropertyWithServices { get; set; }
    }
}