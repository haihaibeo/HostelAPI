using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HostelWebAPI.DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HostelWebAPI.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private IDbRepo repo;

        public ReviewsController(IDbRepo repo)
        {
            this.repo = repo;
        }

        public class ReviewRequest
        {
            public string PropertyId { get; set; }
            public string? ReservationId { get; set; }
            public int StarCount { get; set; }
            public string? ReviewComment { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> PostReview([FromBody] ReviewRequest req)
        {
            if (ModelState.IsValid)
            {
                var prop = await repo.Properties.GetByIdAsync(req.PropertyId);
                if (prop == null) return NotFound(new { message = "Property does not exist" });

                var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var review = await repo.Reviews.GetByUserPropAsync(userId, prop.PropertyId);
                if (review == null) repo.Reviews.Add(review);
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
    }
}