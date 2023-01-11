using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        public ReviewsController(ReviewContext context)
        {
            _context = context;
        }

        public ReviewContext _context { get; set; }

        [HttpGet("reviews")]
        public async Task<List<Review>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Review> GetReview(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        [HttpPost("new")]
        public async Task<Review> AddReview(Review review)
        {
            
                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();
                return review;
            

        }

        [HttpPut("update")]
        public async Task<ActionResult<Review>> UpdateReview(Review review)
        {
            var newReview = await _context.Reviews.FindAsync(review.Id);

            if (newReview != null)
            {
                newReview.Rating = review.Rating;
                newReview.Text = review.Text;
                _context.Reviews.Update(newReview);
                _context.SaveChanges();

                return review;
            }
            return BadRequest("not updated");

        }

        [HttpDelete("delete{id}")]
        public async Task<bool> DeleteReview(int id)
        {
            try
            {
                var newReview = await _context.Reviews.FindAsync(id);
                if (newReview != null)
                {
                    _context.Reviews.Remove(newReview);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;

            }
        }
    }
}
