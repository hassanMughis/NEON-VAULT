using System.ComponentModel.DataAnnotations;

namespace Neon_vault.Models
{
    /// <summary>
    /// Represents a completed purchase order.
    /// </summary>
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(200)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string CustomerEmail { get; set; } = string.Empty;

        [MaxLength(100)]
        public string SessionId { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Range(0, 999999.99)]
        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string PaymentStatus { get; set; } = "Completed";

        // Navigation property
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
