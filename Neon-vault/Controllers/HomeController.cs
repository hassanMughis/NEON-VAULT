using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neon_vault.Data;

namespace Neon_vault.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _db.Games.OrderBy(g => g.Title).ToListAsync();
            return View(games);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
