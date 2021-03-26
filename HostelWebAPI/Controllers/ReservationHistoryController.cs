using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HostelWebAPI.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationHistoryController : ControllerBase
    {
        private readonly IDbRepo repo;
        private readonly UserManager<User> userManager;

        public ReservationHistoryController(IDbRepo repo, UserManager<User> userManager)
        {   
            this.repo = repo;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationByPropertyId([FromQuery(Name = "propertyId")] string propertyId)
        {
            var reserv = await repo.ReservationHistories.GetByPropertyIdAsync(propertyId);
            return Ok(reserv);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetReservationByUserId()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var reserv = await repo.ReservationHistories.GetByUserIdAsync(user.Id);
                return Ok(reserv);
            }
            return BadRequest();
        }

        public class ReservationRequest
        {
            public string PropertyId { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
        }

        [HttpPost]
        [Authorize(Roles = "User, Owner, Admin")]
        public async Task<IActionResult> Reserve([FromBody] ReservationRequest request)
        {
            var from = DateTime.Parse(request.FromDate);
            if (from < DateTime.Today) return BadRequest("Can't choose date in the past");

            var property = await repo.Properties.GetByIdAsync(request.PropertyId);
            var reservDatetime1 = await repo.ReservationHistories.GetByPropertyIdAsync(request.PropertyId);
            var reservDatetime = property.ReservationHistories;

            if (reservDatetime != null && reservDatetime.Count != 0)
            {
                var to = DateTime.Parse(request.ToDate);

                foreach (var dt in reservDatetime)
                {
                    if (!UserCanBookFromTo((from, to), new[] { (dt.FromDate, dt.ToDate) }, null)) return BadRequest("Can't book!");
                }
            }


            return Ok("Can book");
        }
#nullable enable
        // TODO: Extract to business logic
        private bool UserCanBookFromTo((DateTime from, DateTime to) wantDate, (DateTime from, DateTime to)[] reserved, DateTime[]? dayoff)
        {
            if (wantDate.from < DateTime.Today) return false;

            if (wantDate.from.Date >= wantDate.to.Date) return false;

            foreach (var r in reserved)
            {
                if ((wantDate.from < r.to && wantDate.from > r.from) || (wantDate.to > r.from && wantDate.to < r.to))
                    return false;

                if (wantDate.from >= r.from && wantDate.to <= r.to) return false;

                if (wantDate.from <= r.from && wantDate.to >= r.to) return false;
            }

            if (dayoff != null)
                foreach (var d in dayoff)
                {
                    if (d.Date > wantDate.from.Date && d.Date < wantDate.to.Date) return false;
                }

            return true;
        }
#nullable disable
    }
}
