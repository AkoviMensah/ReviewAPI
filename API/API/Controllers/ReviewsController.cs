using API.Data;
using API.Dtos;
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
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound("Review not found");
            return review;
        }

        [HttpPost("new")]
        public async Task<ActionResult<Review>> AddReview(ReviewDto review)
        {
            var data = new Review();
            data.Rating = review.Rating;
            data.Text = review.Text;
            await _context.Reviews.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
            

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
            return NotFound("Review to update not found in DB");

        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            var dBReview = await _context.Reviews.FindAsync(id);
            if (dBReview == null)  return NotFound("Review to delete not found in DB");
            _context.Reviews.Remove(dBReview);
            await _context.SaveChangesAsync();

            return dBReview;
        }
    }
}
