using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.Models;
using HostelWebAPI.Services;
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
        private readonly IUserService userService;

        public PropertiesController(IDbRepo repo, Services.IUserService userService)
        {
            this.repo = repo;
            this.userService = userService;
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
                var reviews = await repo.Reviews.GetByPropAsync(r.Id);
                r.TotalReview = reviews.Count;
                r.TotalStar = reviews.Sum(r => r.Star);
                r.Services = new List<ServiceResponse>();
                var services = await repo.Properties.GetServices(r.Id);
                foreach (var s in services)
                {
                    r.Services.Add(new ServiceResponse(s));
                }
            }

            return Ok(resp);
        }

        // Get all properties published by owner
        [HttpGet("host")]
        public async Task<IActionResult> GetOwnerProps()
        {
            var user = await userService.GetCurrentUserAsync(HttpContext.User);
            if (user == null) return Unauthorized(new { message = "Couldn't find user" });

            var allProps = await repo.Properties.GetAllAsync();
            var props = allProps.Where(p => p.OwnerId == user.Id).ToList().Select(p => new PropertyViewResponse(p));

            foreach (var p in props)
            {
                var services = await repo.Properties.GetServices(p.Id);
                services.ForEach(s => p.Services.Add(new ServiceResponse(s)));
            }

            return Ok(props);
        }

        [HttpGet("saved")]
        public async Task<IActionResult> GetSavedProps()
        {
            var user = await userService.GetCurrentUserAsync(HttpContext.User);
            if (user == null) return Unauthorized(new { message = "Couldn't find user" });

            var likes = await repo.Likes.GetAllByUserIdAsync(user.Id);
            var props = likes.Select(l => new PropertyViewResponse(l.Property)).ToList();

            return Ok(props);
        }

        // Get property by id
        // GET api/<PropertyController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id, [FromQuery] string isEditing)
        {
            var property = await repo.Properties.GetByIdAsync(id);
            if (property == null) return NotFound(new { message = "Property not found" });

            var schedules = await repo.ReservationHistories.GetReservationSchedule(id, 3);

            var resp = new PropertyResponse(property, schedules);
            var owner = await userService.GetUserByIdAsync(property.OwnerId);
            resp.OwnerInfo = new UserInfoResponse(owner);

            var services = await repo.Properties.GetServices(id);
            foreach (var s in services)
            {
                resp.Services.Add(new ServiceResponse(s));
            }

            var reviews = await repo.Reviews.GetByPropAsync(id);
            resp.TotalReview = reviews.Count;
            resp.TotalStar = reviews.Sum(r => r.Star);

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await userService.GetCurrentUserAsync(HttpContext.User);
                var didLike = await repo.Likes.GetByPropertyIdAsync(property.PropertyId, user.Id);

                if (didLike != null) resp.Liked = true;
                else resp.Liked = false;
            }
            return Ok(resp);
        }

        [HttpPut("validate-prop")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> ValidateProp([FromBody] ValidationRequest req)
        {
            var prop = await repo.Properties.GetByIdAsync(req.PropId);
            if (prop == null) return NotFound();

            // TODO: check for prop status id
            prop.PropertyStatusId = req.PropStatusId;
            repo.Properties.Update(prop);

            var res = await repo.SaveChangesAsync();
            return Ok(res);
        }

        [HttpPut("{propId}")]
        [Authorize(Roles = AppRoles.Owner)]
        public async Task<IActionResult> UpdateProperty([FromBody] PublishPropertyRequest request, [FromRoute] string propId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await userService.GetCurrentUserAsync(HttpContext.User);
            var prop = await repo.Properties.GetByIdAsync(propId);
            if (prop.OwnerId != user.Id) return BadRequest();

            return Ok();
        }

        // POST api/<PropertyController>
        [HttpPost]
        [Authorize(Roles = AppRoles.Owner)]
        public async Task<IActionResult> Post([FromBody] PublishPropertyRequest request)
        {
            if (ModelState.IsValid)
            {
                var addr = new PropertyAddress();
                var prop = new Property();
                var images = new List<Image>();
                var services = new PropertyService();
                var user = await userService.GetCurrentUserAsync(HttpContext.User);

                addr.CityId = request.CityId;

                prop.PropertyId = Guid.NewGuid().ToString();
                addr.PropertyId = prop.PropertyId;
                services.PropertyId = prop.PropertyId;

                prop.Name = request.Name;
                prop.Description = request.Description;
                prop.Introduction = request.Introduction;
                prop.MaxPeople = request.MaxGuest;
                prop.PricePerNight = request.BasePrice;
                prop.PropertyTypeId = request.PropTypeId;
                prop.CleaningFee = request.CleaningFee;
                prop.ServiceFee = request.ServiceFee;
                prop.TotalReview = 0;
                prop.TotalStar = 0;
                prop.OwnerId = user.Id;
                addr.Number = request.Number;
                addr.StreetName = request.StreetName;
                addr.Description = request.AddressDesc;

                foreach (var i in request.Images)
                {
                    var image = new Image();
                    image.ImageId = Guid.NewGuid().ToString();
                    image.Alt = i.Alt;
                    image.DeleteHash = i.DeleteHash;
                    image.PropertyId = prop.PropertyId;
                    image.Url = i.Url;
                    images.Add(image);
                }
                repo.Properties.Add(prop, addr, images, services);
                var saved = await repo.SaveChangesAsync();
                if (saved > 0)
                    return Ok("Successfully added new property");
                else return BadRequest(saved);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("services")]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await repo.Services.GetAllAsync();

            var resp = new List<ServiceResponse>();
            services.ForEach(s => resp.Add(new ServiceResponse(s)));
            return Ok(resp);
        }
    }
}
