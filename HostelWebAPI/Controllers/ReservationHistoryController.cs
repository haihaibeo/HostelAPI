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
    [Route("api/reservation")]
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
    }
}
