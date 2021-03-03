using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class PropertyType
    {
        public PropertyType()
        {
            Properties = new HashSet<Property>();
        }

        [Key]
        [Column("PropertyTypeID")]
        [StringLength(50)]
        public string PropertyTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
