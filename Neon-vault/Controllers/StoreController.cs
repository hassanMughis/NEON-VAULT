using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neon_vault.Data;

namespace Neon_vault.Controllers
{
    /// <summary>
    /// Handles the game store browsing: Home, All Games, and Product Details.
    /// </summary>
    public class StoreController : Controller
    {
        private readonly AppDbContext _db;

        public StoreController(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>Store home — displays all games in shelves.</summary>
        public async Task<IActionResult> Index()
        {
            var games = await _db.Games.OrderBy(g => g.Title).ToListAsync();
            return View(games);
        }

        /// <summary>All games page with search and genre filter support.</summary>
        public async Task<IActionResult> AllGames(string? search, string? genre)
        {
            var query = _db.Games.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(g => g.Title.Contains(search) || g.Description.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(genre) && genre != "All")
            {
                query = query.Where(g => g.Genre == genre);
            }

            var games = await query.OrderBy(g => g.Title).ToListAsync();
            var genres = await _db.Games.Select(g => g.Genre).Distinct().OrderBy(g => g).ToListAsync();

            ViewBag.Search = search;
            ViewBag.SelectedGenre = genre ?? "All";
            ViewBag.Genres = genres;

            return View(games);
        }

        /// <summary>Product details page for a single game.</summary>
        public async Task<IActionResult> ProductDetails(Guid? id)
        {
            if (id == null) return RedirectToAction("AllGames");

            var game = await _db.Games.FindAsync(id.Value);
            if (game == null) return NotFound();

            // Check if already in cart or library for this session
            var sessionId = GetSessionId();
            ViewBag.InCart = await _db.CartItems.AnyAsync(c => c.SessionId == sessionId && c.GameId == id.Value);
            ViewBag.InLibrary = await _db.LibraryItems.AnyAsync(l => l.SessionId == sessionId && l.GameId == id.Value);

            return View(game);
        }

        /// <summary>Helper to get or create a persistent session identifier.</summary>
        private string GetSessionId()
        {
            var sessionId = HttpContext.Session.GetString("CartSessionId");
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("CartSessionId", sessionId);
            }
            return sessionId;
        }
    }
}
