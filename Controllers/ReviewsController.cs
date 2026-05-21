using GameReviewAPI.Data;
using GameReviewAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameReviewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/reviews — get all reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _context.Reviews.Include(r => r.Game).ToListAsync();
        }

        // GET api/reviews/5 — get one review by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews.Include(r => r.Game)
                                               .FirstOrDefaultAsync(r => r.Id == id);
            if (review == null) return NotFound();
            return review;
        }

        // GET api/reviews/game/5 — get all reviews for a specific game
        [HttpGet("game/{gameId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByGame(int gameId)
        {
            return await _context.Reviews
                .Where(r => r.GameId == gameId)
                .Include(r => r.Game)
                .ToListAsync();
        }

        // POST api/reviews — submit a new review
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            // Check the game actually exists before adding a review for it
            var game = await _context.Games.FindAsync(review.GameId);
            if (game == null) return NotFound("Game not found");

            review.CreatedAt = DateTime.UtcNow;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
        }

        // DELETE api/reviews/5 — delete a review
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}