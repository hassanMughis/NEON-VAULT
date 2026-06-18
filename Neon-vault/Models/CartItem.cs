using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neon_vault.Models
{
    /// <summary>
    /// Represents an item in a user's shopping cart.
    /// Uses SessionId (browser cookie) to track anonymous carts.
    /// </summary>
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string SessionId { get; set; } = string.Empty;

        [Required]
        public Guid GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game? Game { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
