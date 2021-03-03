using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class City
    {
        public City()
        {
            PropertyAddresses = new HashSet<PropertyAddress>();
        }

        [Key]
        [Column("CityID")]
        [StringLength(50)]
        public string CityId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }

        public virtual ICollection<PropertyAddress> PropertyAddresses { get; set; }
    }
}
