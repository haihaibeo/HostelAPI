using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI
{
    public static class AppRoles
    {
        public const string User = "User";
        public const string Owner = "Owner";
        public const string Admin = "Admin";
    }

    public static class ReservationStatus
    {
        public const string OnReserved = "1";
        public const string Active = "2";
        public const string Completed = "3";
    }

    public enum PaymentStatus
    {
        Paid = 1,
        UnPaid = 2
    }

    public static class AppPolicies
    {
        public const string RequiredOwnerRole = "RequiredOwnerRole";
        public const string RequiredAdminRole = "RequiredAdminRole";
        public const string CanDeleteReservation = "CanDeleteReservation";
        public const string OwnerDeleteProperty = "OwnerDeleteProperty";
    }
}
