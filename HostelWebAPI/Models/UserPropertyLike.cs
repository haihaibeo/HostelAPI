using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.Models
{
    public partial class UserPropertyLike
    {
        public UserPropertyLike()
        {

        }

        [Key]
        [Required]
        [MaxLength(50)]
        public string UserPropertyId { get; set; }

        [MaxLength(50)]
        public string PropertyId { get; set; }

        [MaxLength(450)]
        public string UserId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
