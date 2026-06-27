using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neon_vault.Data;
using Neon_vault.Helpers;
using Neon_vault.Models;
using System.IO;

namespace Neon_vault.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public AccountController(AppDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var userId = HttpContext.Session.GetString("CurrentUserId");
            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                return RedirectToAction("Register");
            }

            var user = await _db.ChatUsers.FirstOrDefaultAsync(u => u.Id == parsedUserId);
            if (user == null)
            {
                return RedirectToAction("Register");
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(ChatUser model, IFormFile? profileImage, string password)
        {
            var userId = HttpContext.Session.GetString("CurrentUserId");
            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                return RedirectToAction("Register");
            }

            var user = await _db.ChatUsers.FirstOrDefaultAsync(u => u.Id == parsedUserId);
            if (user == null)
            {
                return RedirectToAction("Register");
            }

            user.DisplayName = model.DisplayName;
            user.Email = model.Email;
            user.Bio = model.Bio;
            user.Username = string.IsNullOrWhiteSpace(model.Username) ? user.Username : model.Username;
            if (!string.IsNullOrWhiteSpace(password))
            {
                user.PasswordHash = SecurityHelper.HashPassword(password);
            }

            if (profileImage != null && profileImage.Length > 0)
            {
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp", "image/gif" };
                if (!allowedTypes.Contains(profileImage.ContentType.ToLowerInvariant()))
                {
                    ModelState.AddModelError(string.Empty, "Please upload a valid image file.");
                    return View("Settings", user);
                }

                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "profiles");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(profileImage.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }

                if (!string.IsNullOrWhiteSpace(user.ProfileImageUrl))
                {
                    var oldPath = Path.Combine(_environment.WebRootPath, user.ProfileImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                user.ProfileImageUrl = $"/uploads/profiles/{fileName}";
            }

            await _db.SaveChangesAsync();
            HttpContext.Session.SetString("CurrentUserName", string.IsNullOrWhiteSpace(user.DisplayName) ? user.Username : user.DisplayName);
            HttpContext.Session.SetString("CurrentUserUsername", user.Username);
            HttpContext.Session.SetString("CurrentUserProfileImageUrl", user.ProfileImageUrl ?? string.Empty);
            TempData["Message"] = "Profile updated successfully.";
            return RedirectToAction("Settings");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new ChatUser());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ChatUser model, string password)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Username and password are required.");
                return View(model);
            }

            if (await _db.ChatUsers.AnyAsync(u => u.Username.ToLower() == model.Username.Trim().ToLower() && !u.IsTemporaryGuest))
            {
                ModelState.AddModelError(string.Empty, "That username is already taken.");
                return View(model);
            }

            var sessionId = GetSessionId();
            var guestUser = HttpContext.Session.GetString("IsAdmin") == "true"
                ? null
                : await _db.ChatUsers.FirstOrDefaultAsync(u => u.SessionId == sessionId && u.IsTemporaryGuest);
            ChatUser user;

            if (guestUser != null)
            {
                guestUser.Username = model.Username.Trim();
                guestUser.DisplayName = model.DisplayName;
                guestUser.Email = model.Email;
                guestUser.Bio = model.Bio;
                guestUser.PasswordHash = SecurityHelper.HashPassword(password);
                guestUser.IsTemporaryGuest = false;
                guestUser.IsAdmin = false;
                guestUser.IsActive = true;
                user = guestUser;
            }
            else
            {
                user = new ChatUser
                {
                    Username = model.Username.Trim(),
                    DisplayName = model.DisplayName,
                    Email = model.Email,
                    Bio = model.Bio,
                    PasswordHash = SecurityHelper.HashPassword(password),
                    IsTemporaryGuest = false,
                    IsAdmin = false,
                    IsActive = true,
                    SessionId = sessionId,
                    AvatarColorHex = "#DDB7FF"
                };
                _db.ChatUsers.Add(user);
            }

            await _db.SaveChangesAsync();

            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                TempData["Message"] = "Account created successfully.";
                return RedirectToAction("Users", "Admin");
            }

            HttpContext.Session.SetString("CurrentUserId", user.Id.ToString());
            HttpContext.Session.SetString("CurrentUserName", string.IsNullOrWhiteSpace(user.DisplayName) ? user.Username : user.DisplayName);
            HttpContext.Session.SetString("CurrentUserUsername", user.Username);
            HttpContext.Session.SetString("CurrentUserProfileImageUrl", user.ProfileImageUrl ?? string.Empty);
            HttpContext.Session.SetString("IsAdmin", "false");
            TempData["Message"] = "Account created successfully.";
            return RedirectToAction("Settings");
        }

        private string GetSessionId()
        {
            var sessionId = Request.Cookies["CartSessionId"];
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                Response.Cookies.Append("CartSessionId", sessionId, new CookieOptions { Expires = DateTime.Now.AddDays(30) });
            }
            return sessionId;
        }
    }
}
