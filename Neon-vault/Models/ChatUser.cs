using System.ComponentModel.DataAnnotations;

namespace Neon_vault.Models
{
    public class ChatUser
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(7)]
        public string AvatarColorHex { get; set; } = "#DDB7FF";

        public bool IsTemporaryGuest { get; set; } = true;

        [Required, MaxLength(100)]
        public string SessionId { get; set; } = string.Empty;
    }
}
