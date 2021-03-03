using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class PaymentStatus
    {
        public PaymentStatus()
        {
            ReservationHistories = new HashSet<ReservationHistory>();
        }

        [Key]
        [Column("PaymentStatusID")]
        [StringLength(50)]
        public string PaymentStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public virtual ICollection<ReservationHistory> ReservationHistories { get; set; }
    }
}
