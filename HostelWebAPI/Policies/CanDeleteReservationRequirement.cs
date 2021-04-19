using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HostelWebAPI.Policies
{
    public class CanDeleteReservationRequirement : IAuthorizationRequirement
    {
        public string UserId { get; set; }
        public string PropertyId { get; set; }
        public CanDeleteReservationRequirement(string userId, string propertyId)
        {
            UserId = userId;
            PropertyId = propertyId;
        }
    }

    public class CanDeleteResertionHandler : AuthorizationHandler<CanDeleteReservationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanDeleteReservationRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
