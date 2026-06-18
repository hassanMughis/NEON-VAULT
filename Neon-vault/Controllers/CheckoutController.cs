using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neon_vault.Data;
using Neon_vault.Models;

namespace Neon_vault.Controllers
{
    /// <summary>
    /// Handles checkout flow: review cart, complete purchase, display receipt.
    /// </summary>
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _db;

        public CheckoutController(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>Checkout page showing cart items and payment form.</summary>
        public async Task<IActionResult> Index()
        {
            var sessionId = GetSessionId();
            var cartItems = await _db.CartItems
                .Include(c => c.Game)
                .Where(c => c.SessionId == sessionId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Message"] = "Your cart is empty!";
                return RedirectToAction("Index", "Store");
            }

            ViewBag.Total = cartItems.Sum(c => c.Game?.Price ?? 0);
            return View(cartItems);
        }

        /// <summary>
        /// Complete the purchase: create Order + OrderItems, move games to Library, clear cart.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(string customerName, string customerEmail)
        {
            var sessionId = GetSessionId();
            var cartItems = await _db.CartItems
                .Include(c => c.Game)
                .Where(c => c.SessionId == sessionId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Message"] = "Your cart is empty!";
                return RedirectToAction("Index", "Store");
            }

            // Create the order
            var order = new Order
            {
                CustomerName = customerName,
                CustomerEmail = customerEmail,
                SessionId = sessionId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = cartItems.Sum(c => c.Game?.Price ?? 0),
                PaymentStatus = "Completed"
            };

            // Create order items (receipt line items)
            foreach (var item in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    GameId = item.GameId,
                    PriceAtPurchase = item.Game?.Price ?? 0
                });

                // Add to library (user now owns this game)
                _db.LibraryItems.Add(new LibraryItem
                {
                    SessionId = sessionId,
                    GameId = item.GameId,
                    PurchaseDate = DateTime.UtcNow
                });
            }

            _db.Orders.Add(order);

            // Clear the cart
            _db.CartItems.RemoveRange(cartItems);

            await _db.SaveChangesAsync();

            return RedirectToAction("Receipt", new { id = order.Id });
        }

        /// <summary>Display the order receipt after successful checkout.</summary>
        public async Task<IActionResult> Receipt(Guid id)
        {
            var order = await _db.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Game)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
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
