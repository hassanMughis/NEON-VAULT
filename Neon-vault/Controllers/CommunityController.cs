using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neon_vault.Data;
using Neon_vault.Models;

namespace Neon_vault.Controllers
{
    public class CommunityController : Controller
    {
        private readonly AppDbContext _db;

        public CommunityController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var sessionId = GetSessionId();
            var user = await GetOrCreateCurrentUserAsync(sessionId);

            if (!await _db.Channels.AnyAsync())
            {
                _db.Channels.Add(new Channel { Name = "General", Category = "Main" });
                _db.Channels.Add(new Channel { Name = "Hardware Setup", Category = "Support" });
                _db.Channels.Add(new Channel { Name = "LFG", Category = "Gaming" });
                await _db.SaveChangesAsync();
            }

            if (!await _db.Messages.AnyAsync())
            {
                await SeedDummyMessages();
            }

            var channels = await _db.Channels.ToListAsync();
            var products = await _db.Games.ToListAsync();

            ViewBag.SessionId = sessionId;
            ViewBag.CurrentUser = user;
            ViewBag.Products = products;

            return View(channels);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages(Guid channelId)
        {
            var messages = await _db.Messages
                .Where(m => m.ChannelId == channelId)
                .Include(m => m.User)
                .Include(m => m.Product)
                .OrderBy(m => m.Timestamp)
                .Take(100)
                .Select(m => new
                {
                    id = m.Id,
                    userId = m.UserId.ToString().ToLowerInvariant(),
                    username = m.User.Username,
                    avatarColor = m.User.AvatarColorHex,
                    body = m.Body,
                    timestamp = m.Timestamp,
                    attachmentUrl = m.AttachmentUrl,
                    product = m.Product != null ? new { m.Product.Id, m.Product.Title, m.Product.CoverImageUrl, m.Product.Price } : null
                })
                .ToListAsync();

            return Json(messages);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAttachment(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            if (file.Length > 5 * 1024 * 1024)
                return BadRequest("File exceeds 5MB limit");

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads"));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Json(new { url = "/uploads/" + fileName });
        }

        private async Task<ChatUser> GetOrCreateCurrentUserAsync(string sessionId)
        {
            var currentUserId = HttpContext.Session.GetString("CurrentUserId");
            if (Guid.TryParse(currentUserId, out var parsedUserId))
            {
                var sessionUser = await _db.ChatUsers.FirstOrDefaultAsync(u => u.Id == parsedUserId);
                if (sessionUser != null)
                {
                    sessionUser.SessionId = sessionId;
                    await _db.SaveChangesAsync();
                    return sessionUser;
                }
            }

            var user = await _db.ChatUsers.FirstOrDefaultAsync(u => u.SessionId == sessionId);
            if (user == null)
            {
                user = new ChatUser
                {
                    SessionId = sessionId,
                    Username = "Guest_" + Guid.NewGuid().ToString().Substring(0, 5),
                    IsTemporaryGuest = true,
                    AvatarColorHex = GetRandomHexColor()
                };
                _db.ChatUsers.Add(user);
                await _db.SaveChangesAsync();
            }

            HttpContext.Session.SetString("CurrentUserId", user.Id.ToString());
            return user;
        }

        private async Task SeedDummyMessages()
        {
            var user1 = new ChatUser
            {
                Username = "NeonGamer42",
                AvatarColorHex = "#FF6B9D",
                IsTemporaryGuest = false,
                SessionId = "seed_user_1"
            };
            var user2 = new ChatUser
            {
                Username = "VoidHunter",
                AvatarColorHex = "#4ECDC4",
                IsTemporaryGuest = false,
                SessionId = "seed_user_2"
            };
            var user3 = new ChatUser
            {
                Username = "PixelQueen",
                AvatarColorHex = "#FFE66D",
                IsTemporaryGuest = false,
                SessionId = "seed_user_3"
            };
            var user4 = new ChatUser
            {
                Username = "CyberWolf_X",
                AvatarColorHex = "#A8E6CF",
                IsTemporaryGuest = false,
                SessionId = "seed_user_4"
            };

            _db.ChatUsers.AddRange(user1, user2, user3, user4);
            await _db.SaveChangesAsync();

            var generalChannel = await _db.Channels.FirstOrDefaultAsync(c => c.Name == "General");
            var hardwareChannel = await _db.Channels.FirstOrDefaultAsync(c => c.Name == "Hardware Setup");
            var lfgChannel = await _db.Channels.FirstOrDefaultAsync(c => c.Name == "LFG");

            if (generalChannel == null || hardwareChannel == null || lfgChannel == null) return;

            var baseTime = DateTime.UtcNow.AddHours(-3);

            var messages = new List<Message>
            {
                new Message { ChannelId = generalChannel.Id, UserId = user1.Id, Body = "Hey everyone! Just picked up Neon Phantom, the cyberpunk RPG vibes are insane 🔥", Timestamp = baseTime },
                new Message { ChannelId = generalChannel.Id, UserId = user2.Id, Body = "Dude same! The neural implant upgrade system is so deep. I'm already 20 hours in", Timestamp = baseTime.AddMinutes(2) },
                new Message { ChannelId = generalChannel.Id, UserId = user3.Id, Body = "Wait is that the open-world one? I've been eyeing Arcane Rift instead", Timestamp = baseTime.AddMinutes(5) },
                new Message { ChannelId = generalChannel.Id, UserId = user1.Id, Body = "Both are amazing honestly. Arcane Rift has better combat but Neon Phantom's story is next level", Timestamp = baseTime.AddMinutes(7) },
                new Message { ChannelId = generalChannel.Id, UserId = user4.Id, Body = "Just joined the community! Anyone playing Void Runners? Looking for squad mates", Timestamp = baseTime.AddMinutes(15) },
                new Message { ChannelId = generalChannel.Id, UserId = user2.Id, Body = "Welcome! Check the LFG channel, there's usually people looking for groups there 👊", Timestamp = baseTime.AddMinutes(17) },
                new Message { ChannelId = generalChannel.Id, UserId = user3.Id, Body = "Has anyone seen the new hardware drops? That RTX 6090 is absolutely wild", Timestamp = baseTime.AddMinutes(30) },
                new Message { ChannelId = generalChannel.Id, UserId = user1.Id, Body = "Yeah the price tag is wild too lol. But for true path tracing it's worth it", Timestamp = baseTime.AddMinutes(32) },
                new Message { ChannelId = hardwareChannel.Id, UserId = user3.Id, Body = "Anyone running the Nexus Pro Core Console? How's the 4K 120fps performance?", Timestamp = baseTime.AddMinutes(10) },
                new Message { ChannelId = hardwareChannel.Id, UserId = user4.Id, Body = "Got mine last week. Absolutely silent and the SSD load times are insane. Games load in under 2 seconds", Timestamp = baseTime.AddMinutes(12) },
                new Message { ChannelId = hardwareChannel.Id, UserId = user1.Id, Body = "I paired it with the Tactical Comm Pack headset. The 7.1 surround is chef's kiss 🎧", Timestamp = baseTime.AddMinutes(20) },
                new Message { ChannelId = hardwareChannel.Id, UserId = user2.Id, Body = "Is the Elite Controller worth the upgrade from the standard one?", Timestamp = baseTime.AddMinutes(25) },
                new Message { ChannelId = hardwareChannel.Id, UserId = user4.Id, Body = "100%. The trigger stops and customizable paddles completely changed my FPS gameplay", Timestamp = baseTime.AddMinutes(27) },
                new Message { ChannelId = lfgChannel.Id, UserId = user4.Id, Body = "LFG Void Runners ranked! Need 2 more for a 4-stack. Gold rank+", Timestamp = baseTime.AddMinutes(40) },
                new Message { ChannelId = lfgChannel.Id, UserId = user2.Id, Body = "I'm Platinum 2, main support class. Down to run some games!", Timestamp = baseTime.AddMinutes(42) },
                new Message { ChannelId = lfgChannel.Id, UserId = user1.Id, Body = "Count me in. Diamond 1 assault main. Let's go 🚀", Timestamp = baseTime.AddMinutes(45) },
                new Message { ChannelId = lfgChannel.Id, UserId = user3.Id, Body = "Anyone doing the Dark Descent dungeon runs? Looking for a co-op partner for the hardest difficulty", Timestamp = baseTime.AddMinutes(60) }
            };

            _db.Messages.AddRange(messages);
            await _db.SaveChangesAsync();
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

        private string GetRandomHexColor()
        {
            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));
            return color;
        }
    }
}
