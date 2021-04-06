using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class ReservationHistory
    {
        [Key]
        [Column("ReservationID")]
        [StringLength(50)]
        public string ReservationId { get; set; }

        [Column("PropertyID")]
        [StringLength(50)]
        public string PropertyId { get; set; }

        [Column("UserID")]
        [MaxLength(450)]
        public string UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ToDate { get; set; }

        public int AdultNum { get; set; }

        public int ChildrenNum { get; set; }

        public int InfantNum { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TimeCreated { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalCost { get; set; }

        [Required]
        [Column("ReservationStatusID")]
        [StringLength(50)]
        public string ReservationStatusId { get; set; }

        [Required]
        [Column("PaymentStatusID")]
        [StringLength(50)]
        public string PaymentStatusId { get; set; }

        [ForeignKey(nameof(PaymentStatusId))]
        public virtual PaymentStatus PaymentStatus { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; }

        [ForeignKey(nameof(ReservationStatusId))]
        public virtual ReservationStatus ReservationStatus { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
