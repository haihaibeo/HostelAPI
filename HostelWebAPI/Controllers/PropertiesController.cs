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
    [Authorize]
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
        // Get all properties with query search
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] PropertyQueryRequest query)
        {
            var properties = await repo.Properties.GetAllAsync();
            var resp = new List<PropertyViewResponse>();

            if (query.TypeId != null)
                properties = properties.Where(r => r.PropertyTypeId == query.TypeId).ToList();

            if (query.City != null)
            {
                // properties = properties.Where(r => r.PropertyAddress.City.Name.ToLower().Contains(query.City.ToLower())).ToList();
                var found = new List<Property>();
                foreach (var r in properties)
                {
                    if (r.PropertyAddress.City.Name.ToLower().Contains(query.City.ToLower()))
                        found.Add(r);
                }
                properties = found;
            }

            if (query.GuestNum > 0)
                properties = properties.Where(p => p.MaxPeople >= query.GuestNum).ToList();

            if (query.From != null && query.To != null)
            {
                var from = DateTime.Parse(query.From);
                var to = DateTime.Parse(query.To);
                properties = properties.Where(p => repo.ReservationHistories.CanUserBookWithDate(new ReservedDate(from, to), p.PropertyId).Result).ToList();
            }

            resp = properties.Select(p => new PropertyViewResponse(p)).ToList();
            foreach (var r in resp)
            {
                var star = r.TotalStar;
                var review = r.TotalReview;
                repo.Properties.CountStarTotalReview(out star, out review, r.Id);
                r.TotalStar = star;
                r.TotalReview = review;
            }

            return Ok(resp);
        }

        // Get all properties published by user
        [HttpGet("user")]
        public async Task<IActionResult> GetUserProps()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return Unauthorized(new { message = "Couldn't find user" });

            var allProps = await repo.Properties.GetAllAsync();
            var props = allProps.Where(p => p.OwnerId == user.Id).ToList().Select(p => new PropertyViewResponse(p));

            return Ok(props);
        }

        [HttpGet("saved")]
        public async Task<IActionResult> GetSavedProps()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return Unauthorized(new { message = "Couldn't find user" });

            var likes = await repo.Likes.GetAllByUserIdAsync(user.Id);
            var props = likes.Select(l => new PropertyViewResponse(l.Property)).ToList();

            return Ok(props);
        }

        // Get property by id
        // GET api/<PropertyController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            var property = await repo.Properties.GetByIdAsync(id);
            if (property == null) return NotFound(new { message = "Property not found" });

            var schedules = await repo.ReservationHistories.GetReservationSchedule(id, 3);

            var resp = new PropertyResponse(property, schedules);

            if (HttpContext.User.Identity.IsAuthenticated)
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
        public IActionResult Post([FromBody] PublishPropertyRequest request)
        {
            return Ok("Hello owner");
        }
    }
}
