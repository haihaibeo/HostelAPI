using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        [Key]
        [Column("CountryID")]
        [StringLength(50)]
        public string CountryId { get; set; }
        [Required]
        [StringLength(50)]
        public string CountryName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
