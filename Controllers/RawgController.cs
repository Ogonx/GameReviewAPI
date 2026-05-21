using GameReviewAPI.Data;
using GameReviewAPI.Models;
using GameReviewAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RawgController : ControllerBase
    {
        private readonly RawgService _rawgService;
        private readonly AppDbContext _context;

        public RawgController(RawgService rawgService, AppDbContext context)
        {
            _rawgService = rawgService;
            _context = context;
        }

        // GET api/rawg/search?query=halo
        // Search for games on RAWG by name
        [HttpGet("search")]
        public async Task<IActionResult> SearchGames([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
                return BadRequest("Query cannot be empty");

            var games = await _rawgService.SearchGamesAsync(query);
            return Ok(games);
        }

        // POST api/rawg/import/{rawgId}
        // Pull a game from RAWG and save it to our database
        [HttpPost("import/{rawgId}")]
        public async Task<IActionResult> ImportGame(string rawgId)
        {
            // Check if we already have this game
            var existing = _context.Games.FirstOrDefault(g => g.RawgId == rawgId);
            if (existing != null)
                return Ok(existing);

            // Fetch from RAWG
            var rawgGame = await _rawgService.GetGameByIdAsync(rawgId);
            if (rawgGame == null)
                return NotFound("Game not found on RAWG");

            // Convert RAWG data into our Game model and save it
            var game = new Game
            {
                RawgId = rawgGame.Id,
                Name = rawgGame.Name,
                Slug = rawgGame.Slug,
                BackgroundImage = rawgGame.BackgroundImage,
                RawgRating = rawgGame.Rating,
                Genres = string.Join(", ", rawgGame.Genres.Select(g => g.Name)),
                Platforms = string.Join(", ", rawgGame.Platforms.Select(p => p.Platform.Name))
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", "Games", new { id = game.Id }, game);
        }
    }
}