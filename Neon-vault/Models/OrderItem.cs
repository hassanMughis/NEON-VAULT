using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neon_vault.Models
{
    /// <summary>
    /// Represents a single game within an order (line item on the receipt).
    /// </summary>
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        [Required]
        public Guid GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game? Game { get; set; }

        /// <summary>Price snapshot at the time of purchase.</summary>
        [Range(0, 9999.99)]
        public decimal PriceAtPurchase { get; set; }
    }
}
