using Microsoft.AspNetCore.SignalR;
using Neon_vault.Data;
using Neon_vault.Models;
using System.Text.Json;

namespace Neon_vault.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _db;

        public ChatHub(AppDbContext db)
        {
            _db = db;
        }

        public async Task JoinChannel(string channelId, string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channelId);
        }

        public async Task SendMessage(string channelId, string sessionId, string message, string? attachmentUrl, string? productId)
        {
            var user = _db.ChatUsers.FirstOrDefault(u => u.SessionId == sessionId);
            if (user == null) return;

            Guid? prodId = null;
            if (Guid.TryParse(productId, out var parsedProdId))
            {
                prodId = parsedProdId;
            }

            var chatMessage = new Message
            {
                ChannelId = Guid.Parse(channelId),
                UserId = user.Id,
                Body = message,
                Timestamp = DateTime.UtcNow,
                AttachmentUrl = attachmentUrl,
                ProductId = prodId
            };

            _db.Messages.Add(chatMessage);
            await _db.SaveChangesAsync();

            var productInfo = "";
            if (prodId != null)
            {
                var p = await _db.Games.FindAsync(prodId);
                if (p != null)
                {
                    productInfo = JsonSerializer.Serialize(new { p.Id, p.Title, p.CoverImageUrl, p.Price });
                }
            }

            await Clients.Group(channelId).SendAsync("ReceiveMessage", new
            {
                id = chatMessage.Id,
                userId = user.Id.ToString().ToLowerInvariant(),
                username = user.Username,
                avatarColor = user.AvatarColorHex,
                body = message,
                timestamp = chatMessage.Timestamp,
                attachmentUrl = attachmentUrl,
                product = productInfo
            });
        }
    }
}
