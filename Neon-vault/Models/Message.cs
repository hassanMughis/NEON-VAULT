using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neon_vault.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ChannelId { get; set; }
        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public ChatUser User { get; set; } = null!;

        [Required, MaxLength(2000)]
        public string Body { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? AttachmentUrl { get; set; }

        public Guid? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Game? Product { get; set; }
    }
}
