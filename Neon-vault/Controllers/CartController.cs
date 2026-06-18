using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neon_vault.Data;
using Neon_vault.Models;

namespace Neon_vault.Controllers
{
    /// <summary>
    /// Manages the shopping cart: add, remove, view items.
    /// Uses browser session ID to track anonymous carts.
    /// </summary>
    public class CartController : Controller
    {
        private readonly AppDbContext _db;

        public CartController(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>View all items in the current session's cart.</summary>
        public async Task<IActionResult> Index()
        {
            var sessionId = GetSessionId();
            var cartItems = await _db.CartItems
                .Include(c => c.Game)
                .Where(c => c.SessionId == sessionId)
                .OrderByDescending(c => c.AddedAt)
                .ToListAsync();

            ViewBag.Total = cartItems.Sum(c => c.Game?.Price ?? 0);
            return View(cartItems);
        }

        /// <summary>Add a game to the cart. Prevents duplicates and games already owned.</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(Guid gameId, string? returnUrl)
        {
            var sessionId = GetSessionId();

            // Check if game exists
            var game = await _db.Games.FindAsync(gameId);
            if (game == null) return NotFound();

            // Prevent adding if already in cart
            var alreadyInCart = await _db.CartItems
                .AnyAsync(c => c.SessionId == sessionId && c.GameId == gameId);
            if (alreadyInCart)
            {
                TempData["Message"] = "This game is already in your cart!";
                return Redirect(returnUrl ?? "/Store");
            }

            // Prevent adding if already in library (purchased)
            var alreadyOwned = await _db.LibraryItems
                .AnyAsync(l => l.SessionId == sessionId && l.GameId == gameId);
            if (alreadyOwned)
            {
                TempData["Message"] = "You already own this game!";
                return Redirect(returnUrl ?? "/Store");
            }

            // Add to cart
            var cartItem = new CartItem
            {
                SessionId = sessionId,
                GameId = gameId,
                AddedAt = DateTime.UtcNow
            };

            _db.CartItems.Add(cartItem);
            await _db.SaveChangesAsync();

            TempData["Message"] = $"'{game.Title}' added to cart!";
            return Redirect(returnUrl ?? "/Store");
        }

        /// <summary>Remove a specific item from the cart.</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            var sessionId = GetSessionId();
            var cartItem = await _db.CartItems
                .FirstOrDefaultAsync(c => c.Id == id && c.SessionId == sessionId);

            if (cartItem != null)
            {
                _db.CartItems.Remove(cartItem);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        /// <summary>JSON endpoint returning the cart item count for the header badge.</summary>
        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            var sessionId = GetSessionId();
            var count = await _db.CartItems.CountAsync(c => c.SessionId == sessionId);
            return Json(new { count });
        }

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
