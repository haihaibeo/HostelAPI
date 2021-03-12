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

        [Required]
        [StringLength(100)]
        [DefaultValue("https://picsum.photos/seed/picsum/500/500")]
        public string ThumbnailImg { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
