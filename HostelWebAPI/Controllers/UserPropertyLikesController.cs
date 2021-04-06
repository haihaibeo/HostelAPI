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
    [Route("api/likes")]
    [ApiController]
    public class UserPropertyLikesController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IDbRepo repo;

        public UserPropertyLikesController(UserManager<Models.User> userManager, IDbRepo repo)
        {
            this.userManager = userManager;
            this.repo = repo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);
            var userLikesProp = await repo.Likes.GetAllByUserIdAsync(user.Id);
            return Ok(userLikesProp);
        }

        // POST api/<UserPropertyLikesController>
        [HttpPost("{propertyId}")]
        [Authorize]
        public async Task<IActionResult> ToggleLike([FromRoute] string propertyId)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);
            var property = await repo.Properties.GetByIdAsync(propertyId);

            if (property == null) return NotFound();

            var didLike = await repo.Likes.GetByPropertyIdAsync(propertyId, user.Id);

            if (didLike == null)
            {
                UserPropertyLike like = new UserPropertyLike()
                {
                    PropertyId = property.PropertyId,
                    UserId = user.Id,
                };
                like.UserPropertyId = Guid.NewGuid().ToString();

                repo.Likes.Add(like);
                await repo.Likes.SaveChangeAsync();
                return Ok("liked");
            }
            else
            {
                repo.Likes.DeleteById(didLike.UserPropertyId);
                await repo.Likes.SaveChangeAsync();
                return Ok("unliked");
            }
        }
    }
}
