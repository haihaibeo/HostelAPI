using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class Image
    {
        [Key]
        [Column("ImageID")]
        [StringLength(50)]
        public string ImageId { get; set; }
        [Required]
        [StringLength(100)]
        public string Url { get; set; }
        [Required]
        [Column("PropertyID")]
        [StringLength(50)]
        public string PropertyId { get; set; }
        [StringLength(100)]
        public string Alt { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; }
    }
}
