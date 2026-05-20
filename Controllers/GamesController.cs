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
    }
}
