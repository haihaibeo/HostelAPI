using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class PropertyAddress
    {
        [Key]
        [Column("PropertyID")]
        [StringLength(50)]
        public string PropertyId { get; set; }

        [Required]
        [Column("CityID")]
        [StringLength(50)]
        public string CityId { get; set; }

        [Required]
        [StringLength(50)]
        public string StreetName { get; set; }

        [StringLength(10)]
        public string Number { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; }
    }
}
