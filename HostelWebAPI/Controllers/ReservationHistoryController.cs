using HostelWebAPI.BL;
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
        private readonly IReservationBL rsvBL;

        public ReservationHistoryController(IDbRepo repo, UserManager<User> userManager, IReservationBL reservationBL)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.rsvBL = reservationBL;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetReservationByPropertyId([FromQuery(Name = "propertyId")] string propertyId)
        {
            var reserv = await repo.ReservationHistories.GetByPropertyIdAsync(propertyId);
            return Ok(reserv);
        }

        public class ReservationResponse
        {
            public ReservationResponse(Models.ReservationHistory rs)
            {
                Id = rs.ReservationId;
                Property = new PropertyViewResponse(rs.Property);
                FromDate = rs.FromDate;
                ToDate = rs.ToDate;
                Total = rs.TotalCost;
                PaymentStatus = rs.PaymentStatus.Status;
                ReservationStatus = rs.ReservationStatus.Status;
            }
            public string Id { get; set; }
            public PropertyViewResponse Property { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime CreatedAt { get; set; }
            public decimal Total { get; set; }
            public string PaymentStatus { get; set; }
            public string ReservationStatus { get; set; }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetReservationByUserId()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var reserv = await repo.ReservationHistories.GetByUserIdAsync(user.Id);
                var response = new List<ReservationResponse>(reserv.Select(rs => new ReservationResponse(rs)));
                return Ok(response);
            }
            return BadRequest(new { message = "User's null" });
        }

        // TODO: Extract to business logic
        [HttpPost]
        [Authorize(Roles = "User, Owner, Admin")]
        public async Task<IActionResult> Reserve([FromBody] ReservationRequest request)
        {
            var from = DateTime.Parse(request.FromDate);
            if (from < DateTime.Today) return BadRequest(new { message = "Can't choose date in the past" });

            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);

            var property = await repo.Properties.GetByIdAsync(request.PropertyId);
            if (property == null) return BadRequest("Property does not exist");
            if (request.GuestNum + request.ChildrenNum > property.MaxPeople) return BadRequest("Exceeded max number of people");

            var to = DateTime.Parse(request.ToDate);
            var reservDatetime = property.ReservationHistories;

            if (reservDatetime != null && reservDatetime.Count != 0)
            {
                var dtList = reservDatetime.Select(rs => new ReservedDate(rs)).ToList();
                var canBook = await repo.ReservationHistories.CanUserBookWithDate(new ReservedDate(from, to), property.PropertyId);

                if (!canBook) return BadRequest("Wrong dates!");
                // foreach (var dt in reservDatetime)
                // {
                //     if (!UserCanBookFromTo((from, to), new[] { (dt.FromDate, dt.ToDate) }, null)) return BadRequest("Wrong dates!");
                // }
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


        [HttpGet]
        [Route("check-pricing")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPricingForReservation([FromQuery] ReservationRequest request)
        {
            await Task.Delay(500);
            var from = DateTime.Parse(request.FromDate);
            var to = DateTime.Parse(request.ToDate);
            var prop = await repo.Properties.GetByIdAsync(request.PropertyId);
            if (prop == null) return NotFound("Property not found");

            var res = new CheckPricingResponse();
            res.NightCount = repo.ReservationHistories.CountNight(from, to);

            var percentDiscount = rsvBL.CalculateDiscountPercent(res.NightCount);
            res.DiscountPercent = percentDiscount;

            res.Discount = prop.PricePerNight * res.NightCount * percentDiscount / 100;
            res.PricePerNight = prop.PricePerNight;
            res.CleaningFee = prop.CleaningFee;
            res.ServiceFee = prop.ServiceFee;

            res.TotalCost = res.PricePerNight * res.NightCount + res.CleaningFee + res.ServiceFee - res.Discount;

            return Ok(res);
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
