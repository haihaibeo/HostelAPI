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

    public static class AppPolicies
    {
        public const string RequiredOwnerRole = "RequiredOwnerRole";
        public const string RequiredAdminRole = "RequiredAdminRole";
        public const string CanDeleteReservation = "CanDeleteReservation";
        public const string OwnerDeleteProperty = "OwnerDeleteProperty";
    }
}
