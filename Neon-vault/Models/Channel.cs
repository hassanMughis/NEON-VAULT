using System.ComponentModel.DataAnnotations;

namespace Neon_vault.Models
{
    public class Channel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Category { get; set; } = "General";
    }
}
