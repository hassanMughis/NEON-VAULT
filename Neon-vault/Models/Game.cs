using System.ComponentModel.DataAnnotations;

namespace Neon_vault.Models
{
    /// <summary>
    /// Represents a digital game product in the store catalog.
    /// </summary>
    public class Game
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, 9999.99)]
        public decimal Price { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;

        [MaxLength(200)]
        public string Developer { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Category { get; set; } = "Game";

        [MaxLength(500)]
        public string CoverImageUrl { get; set; } = string.Empty;
    }
}
