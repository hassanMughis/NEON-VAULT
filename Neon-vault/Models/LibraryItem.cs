using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neon_vault.Models
{
    /// <summary>
    /// Represents a game the user has purchased and owns in their library.
    /// </summary>
    public class LibraryItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string SessionId { get; set; } = string.Empty;

        [Required]
        public Guid GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game? Game { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    }
}
