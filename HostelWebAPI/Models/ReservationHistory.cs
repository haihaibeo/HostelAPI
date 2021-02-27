using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class ReservationHistory
    {
        public string ReservationId { get; set; }
        public string PropertyId { get; set; }
        public string UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime TimeCreated { get; set; }
        public decimal TotalCost { get; set; }
        public string ReservationStatusId { get; set; }
        public string PaymentStatusId { get; set; }

        public virtual PaymentStatus PaymentStatus { get; set; }
        public virtual Property Property { get; set; }
        public virtual ReservationStatus ReservationStatus { get; set; }
        public virtual User User { get; set; }
    }
}
