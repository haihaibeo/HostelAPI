using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HostelWebAPI.DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HostelWebAPI.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private IDbRepo repo;

        private Services.IUserService userService;

        public ReviewsController(IDbRepo repo, Services.IUserService userService)
        {
            this.repo = repo;
            this.userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] string propertyId)
        {
            var reviews = await repo.Reviews.GetAllAsync();
            var res = new List<ReviewResponse>();
            if (propertyId != null)
            {
                reviews = reviews.Where(r => r.PropId == propertyId).ToList();
            }

            reviews.ForEach(r => res.Add(new ReviewResponse(r, r.User)));

            return Ok(res);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            var review = await repo.Reviews.GetByIdAsync(id);
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> PostReview([FromBody] ReviewRequest req)
        {
            if (ModelState.IsValid)
            {
                var prop = await repo.Properties.GetByIdAsync(req.PropertyId);
                if (prop == null) return NotFound(new { message = "Property does not exist" });

                var user = await userService.GetCurrentUserAsync(HttpContext.User);

                var review = await repo.Reviews.GetByUserPropAsync(user.Id, prop.PropertyId);
                if (review == null)
                {
                    var rev = new Models.Review(req);
                    rev.UserId = user.Id;
                    repo.Reviews.Add(rev);
                }
                else
                {
                    review.Star = req.StarCount;
                    review.ReviewComment = req.ReviewComment;
                    review.ReservationId = req.ReservationId;
                    repo.Reviews.Update(review);
                }
                var res = await repo.SaveChangesAsync();
                if (res >= 0) return Ok();
                return NotFound();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] string id)
        {
            var user = await userService.GetCurrentUserAsync(HttpContext.User);
            if (user == null) return Unauthorized();
            var review = await repo.Reviews.GetByIdAsync(id);
            if (review == null) return NotFound(new { message = "Review not found" });

            if (review.UserId != user.Id) return Forbid();

            repo.Reviews.Delete(review);

            var res = await repo.SaveChangesAsync();
            if (res > 0) return Ok(new { message = $"Successfully deleted review {review.ReviewId}" });

            return Ok();
        }
    }
}