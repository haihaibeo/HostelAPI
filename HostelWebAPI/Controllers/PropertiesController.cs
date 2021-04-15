using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostelWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/properties")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IDbRepo repo;
        private readonly UserManager<User> userManager;

        public PropertiesController(IDbRepo repo, UserManager<User> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }

        // GET: api/<PropertyController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string typeId)
        {
            var properties = await repo.Properties.GetAllAsync();
            var resp = new List<PropertyViewResponse>();

            if (typeId != null)
                properties = properties.Where(r => r.PropertyTypeId == typeId).ToList();
                
            resp = properties.Select(p => new PropertyViewResponse(p)).ToList();

            return Ok(resp);
        }

        // GET api/<PropertyController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var property = await repo.Properties.GetByIdAsync(id);
            if (property == null) return NotFound(new { message = "Property not found" });

            var schedules = await repo.ReservationHistories.GetReservationSchedule(id, 3);

            var resp = new PropertyResponse(property, schedules);

            if (User.Identity.IsAuthenticated)
            {
                var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var user = await userManager.FindByEmailAsync(email);
                var didLike = await repo.Likes.GetByPropertyIdAsync(property.PropertyId, user.Id);

                if (didLike != null) resp.Liked = true;
                else resp.Liked = false;
            }

            if (property != null) return Ok(resp);
            else return BadRequest();
        }

        // POST api/<PropertyController>
        [HttpPost]
        [Authorize(Roles = AppRoles.Owner)]
        public async Task<IActionResult> Post()
        {
            return Ok("Hello owner");
        }
    }
}
