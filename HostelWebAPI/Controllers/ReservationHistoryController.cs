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
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetReservationByPropertyId([FromQuery(Name = "propertyId")] string propertyId)
        {
            var reserv = await repo.ReservationHistories.GetByPropertyIdAsync(propertyId);
            return Ok(reserv);
        }

        [HttpGet("user")]
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

        // TODO: Extract to business logic
        [HttpPost]
        [Authorize(Roles = "User, Owner, Admin")]
        public async Task<IActionResult> Reserve([FromBody] ReservationRequest request)
        {
            var from = DateTime.Parse(request.FromDate);
            if (from < DateTime.Today) return BadRequest("Can't choose date in the past");

            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);

            var property = await repo.Properties.GetByIdAsync(request.PropertyId);
            if (property == null) return BadRequest("Property does not exist");
            if (request.AdultNum + request.ChildrenNum > property.MaxPeople) return BadRequest("Exceeded max number of people");

            var to = DateTime.Parse(request.ToDate);
            var reservDatetime = property.ReservationHistories;

            if (reservDatetime != null && reservDatetime.Count != 0)
            {

                foreach (var dt in reservDatetime)
                {
                    if (!UserCanBookFromTo((from, to), new[] { (dt.FromDate, dt.ToDate) }, null)) return BadRequest("Wrong dates!");
                }
            }
            var reserve = new ReservationHistory()
            {
                ReservationId = Guid.NewGuid().ToString(),
                FromDate = from,
                ToDate = to,
                UserId = user.Id,
                PropertyId = request.PropertyId,
                PaymentStatusId = "1",
                ReservationStatusId = "1",
                TotalCost = property.PricePerNight + property.CleaningFee + property.ServiceFee
            };

            try
            {
                repo.ReservationHistories.Add(reserve);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            var res = await repo.ReservationHistories.SaveChangeAsync();
            if (res > 0) return Ok();

            return BadRequest("Something's wrong!");
        }

        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] string reservationId)
        {
            var reser = await repo.ReservationHistories.GetByIdAsync(reservationId);
            if (reser == null) return BadRequest(new { message = "Reservation does not exist!" });

            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);

            var property = await repo.Properties.GetByIdAsync(reser.PropertyId);

            // TODO: Extract to business logic
            // Only user who booked this room and the owner can delete this reservation
            if (!(reser.UserId == user.Id || reser.UserId == property.OwnerId))
                return Forbid();

            // Only allow deleting reservation when satisfied reservation status
            if (reser.ReservationStatusId != Context.ReservationStatus.OnReserved)
                return BadRequest(new { message = "This reservation cannot be canceled anymore!" });

            var res = repo.ReservationHistories.DeleteByIdAsync(reservationId);
            await repo.ReservationHistories.SaveChangeAsync();
            return Ok();
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
