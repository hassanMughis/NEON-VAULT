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

        /// <summary>Store catalog page with search and category filtering.</summary>
        public async Task<IActionResult> Index(string? search, string? genre)
        {
            var query = _db.Games.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(g => g.Title.Contains(search) || g.Description.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(genre) && genre != "All" && genre != "Home")
            {
                if (genre == "Games") {
                    query = query.Where(g => g.Category == "Game");
                } else if (genre == "Hardware") {
                    query = query.Where(g => g.Category == "Hardware");
                } else {
                    query = query.Where(g => g.Genre == genre);
                }
            }

            var games = await query.OrderBy(g => g.Title).ToListAsync();
            
            // To build the sidebar we need all games
            var allGames = await _db.Games.ToListAsync();
            ViewBag.AllGames = allGames;

            ViewBag.Search = search;
            ViewBag.SelectedGenre = genre ?? "All";

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
            var sessionId = Request.Cookies["CartSessionId"];
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30),
                    HttpOnly = true,
                    IsEssential = true
                };
                Response.Cookies.Append("CartSessionId", sessionId, cookieOptions);
            }
            return sessionId;
        }
    }
}
