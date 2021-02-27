﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class PaymentStatus
    {
        public PaymentStatus()
        {
            ReservationHistory = new HashSet<ReservationHistory>();
        }

        public string PaymentStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<ReservationHistory> ReservationHistory { get; set; }
    }
}
