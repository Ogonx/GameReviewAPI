using GameReviewAPI.Data;
using GameReviewAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameReviewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GamesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.Include(g => g.Reviews).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games.Include(g => g.Reviews).FirstOrDefaultAsync(g => g.Id == id);
            if (game == null) return NotFound();
            return game;
        }

        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return NotFound();
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET api/games/5/averagescore
        // Returns the average review score for a specific game
        [HttpGet("{id}/averagescore")]
        public async Task<IActionResult> GetAverageScore(int id)
        {
            var game = await _context.Games.Include(g => g.Reviews)
                                           .FirstOrDefaultAsync(g => g.Id == id);
            if (game == null) return NotFound();

            if (!game.Reviews.Any())
                return Ok(new { gameId = id, gameName = game.Name, averageScore = 0, totalReviews = 0 });

            var average = game.Reviews.Average(r => r.Score);
            var total = game.Reviews.Count();

            return Ok(new
            {
                gameId = id,
                gameName = game.Name,
                averageScore = Math.Round(average, 1),
                totalReviews = total
            });
        }
    }
}
