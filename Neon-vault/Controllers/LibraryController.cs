using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neon_vault.Data;

namespace Neon_vault.Controllers
{
    /// <summary>
    /// Displays the user's game library (purchased games only).
    /// </summary>
    public class LibraryController : Controller
    {
        private readonly AppDbContext _db;

        public LibraryController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var sessionId = GetSessionId();
            var libraryItems = await _db.LibraryItems
                .Include(l => l.Game)
                .Where(l => l.SessionId == sessionId)
                .OrderByDescending(l => l.PurchaseDate)
                .ToListAsync();

            return View(libraryItems);
        }

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
