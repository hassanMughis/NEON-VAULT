using System.ComponentModel.DataAnnotations;

namespace Neon_vault.Models
{
    public class ChatUser
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? DisplayName { get; set; }

        [MaxLength(320)]
        public string? Email { get; set; }

        [MaxLength(255)]
        public string? PasswordHash { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; }

        [MaxLength(500)]
        public string? ProfileImageUrl { get; set; }

        [MaxLength(7)]
        public string AvatarColorHex { get; set; } = "#DDB7FF";

        public bool IsTemporaryGuest { get; set; } = true;

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; } = true;

        [Required, MaxLength(100)]
        public string SessionId { get; set; } = string.Empty;
    }
}
