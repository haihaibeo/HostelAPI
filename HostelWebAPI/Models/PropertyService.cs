using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.Models
{
    public class PropertyService
    {
        [Key]
        [Column("PropertyId")]
        [StringLength(50)]
        public string PropertyId { get; set; }

        public bool Wifi { get; set; }

        public bool Kitchen { get; set; }

        public bool Breakfast { get; set; }

        public bool PetAllowed { get; set; }

        public bool FreeParking { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; }
    }
}
