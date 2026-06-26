using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neon_vault.Data;
using Neon_vault.Models;

namespace Neon_vault.Controllers
{
    /// <summary>
    /// Admin panel with hardcoded authentication (admin/admin123).
    /// Provides dashboard metrics, order viewing, and game CRUD.
    /// </summary>
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        // ── Authentication ──────────────────────────────────────────

        [HttpGet]
        public IActionResult Login()
        {
            // Already logged in?
            if (HttpContext.Session.GetString("IsAdmin") == "true")
                return RedirectToAction("Dashboard");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            // Hardcoded admin credentials
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Login");
        }

        // ── Dashboard ───────────────────────────────────────────────

        public async Task<IActionResult> Dashboard()
        {
            if (!IsAdmin()) return RedirectToAction("Login");

            ViewBag.TotalGames = await _db.Games.CountAsync();
            ViewBag.TotalOrders = await _db.Orders.CountAsync();
            ViewBag.TotalRevenue = await _db.Orders.SumAsync(o => (decimal?)o.TotalAmount) ?? 0;
            ViewBag.RecentOrders = await _db.Orders
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .Take(10)
                .ToListAsync();

            return View();
        }

        // ── Orders ──────────────────────────────────────────────────

        public async Task<IActionResult> Orders()
        {
            if (!IsAdmin()) return RedirectToAction("Login");

            var orders = await _db.Orders
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(Guid id)
        {
            if (!IsAdmin()) return RedirectToAction("Login");

            var order = await _db.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Game)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // ── Game CRUD ───────────────────────────────────────────────

        public async Task<IActionResult> Games()
        {
            if (!IsAdmin()) return RedirectToAction("Login");

            var games = await _db.Games.OrderBy(g => g.Title).ToListAsync();
            return View(games);
        }

        [HttpGet]
        public IActionResult CreateGame()
        {
            if (!IsAdmin()) return RedirectToAction("Login");
            return View(new Game());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGame(Game game, IFormFile? CoverImage)
        {
            if (!IsAdmin()) return RedirectToAction("Login");

            // Remove CoverImageUrl from validation since we handle it via file upload
            ModelState.Remove("CoverImageUrl");

            if (ModelState.IsValid)
            {
                game.Id = Guid.NewGuid();

                if (CoverImage != null && CoverImage.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(CoverImage.FileName);
                    var imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    Directory.CreateDirectory(imagesDir);
                    var filePath = Path.Combine(imagesDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await CoverImage.CopyToAsync(stream);
                    }

                    game.CoverImageUrl = "/images/" + fileName;
                }
                else
                {
                    game.CoverImageUrl = "/images/game_card_1.png"; // fallback
                }

                _db.Games.Add(game);
                await _db.SaveChangesAsync();
                TempData["Message"] = $"Product '{game.Title}' created!";
                return RedirectToAction("Games");
            }

            return View(game);
        }

        [HttpGet]
        public async Task<IActionResult> EditGame(Guid id)
        {
            if (!IsAdmin()) return RedirectToAction("Login");

            var game = await _db.Games.FindAsync(id);
            if (game == null) return NotFound();
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGame(Game game, IFormFile? CoverImage)
        {
            if (!IsAdmin()) return RedirectToAction("Login");

            ModelState.Remove("CoverImageUrl");

            if (ModelState.IsValid)
            {
                if (CoverImage != null && CoverImage.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(CoverImage.FileName);
                    var imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    Directory.CreateDirectory(imagesDir);
                    var filePath = Path.Combine(imagesDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await CoverImage.CopyToAsync(stream);
                    }

                    game.CoverImageUrl = "/images/" + fileName;
                }

                _db.Games.Update(game);
                await _db.SaveChangesAsync();
                TempData["Message"] = $"Product '{game.Title}' updated!";
                return RedirectToAction("Games");
            }

            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            if (!IsAdmin()) return RedirectToAction("Login");

            var game = await _db.Games.FindAsync(id);
            if (game != null)
            {
                _db.Games.Remove(game);
                await _db.SaveChangesAsync();
                TempData["Message"] = $"Game '{game.Title}' deleted!";
            }

            return RedirectToAction("Games");
        }

        // ── Helper ──────────────────────────────────────────────────

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }
    }
}
