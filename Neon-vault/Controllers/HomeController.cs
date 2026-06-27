using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neon_vault.Data;
using Neon_vault.Models;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(string name, string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(message))
            {
                return BadRequest();
            }

            var contact = new ContactMessage
            {
                Name = name.Trim(),
                Email = email.Trim(),
                Subject = string.IsNullOrWhiteSpace(subject) ? "(no subject)" : subject.Trim(),
                MessageBody = message.Trim(),
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            _db.ContactMessages.Add(contact);
            await _db.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}
