using Microsoft.EntityFrameworkCore;
using Neon_vault.Models;

namespace Neon_vault.Data
{
    /// <summary>
    /// Entity Framework Core database context for the Neon Vault game store.
    /// Includes seed data for 8 games matching the storefront images.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Game> Games => Set<Game>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<LibraryItem> LibraryItems => Set<LibraryItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for money columns
            modelBuilder.Entity<Game>()
                .Property(g => g.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.PriceAtPurchase)
                .HasPrecision(10, 2);

            // Seed 8 games with fixed GUIDs for reproducibility
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567801"),
                    Title = "Neon Phantom",
                    Description = "Dive into a sprawling cyberpunk metropolis as a rogue hacker with mysterious powers. Uncover corporate conspiracies, upgrade your neural implants, and shape the fate of Neo Shanghai in this award-winning open-world RPG.",
                    Price = 49.99m,
                    Genre = "RPG",
                    ReleaseDate = new DateTime(2025, 11, 15),
                    Developer = "NeonForge Studios",
                    CoverImageUrl = "/images/game_card_1.png"
                },
                new Game
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567802"),
                    Title = "Void Runners",
                    Description = "Fast-paced multiplayer FPS set on abandoned space stations. Team up with friends or go solo in intense 60-player battles across zero-gravity arenas. Features advanced movement mechanics and destructible environments.",
                    Price = 39.99m,
                    Genre = "FPS",
                    ReleaseDate = new DateTime(2026, 1, 22),
                    Developer = "Void Interactive",
                    CoverImageUrl = "/images/game_card_2.png"
                },
                new Game
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567803"),
                    Title = "Arcane Rift",
                    Description = "A breathtaking fantasy adventure through shattered realms where magic and technology collide. Master elemental powers, solve ancient puzzles, and battle mythical creatures in a world teetering on the edge of destruction.",
                    Price = 59.99m,
                    Genre = "Adventure",
                    ReleaseDate = new DateTime(2026, 3, 10),
                    Developer = "Rift Games",
                    CoverImageUrl = "/images/game_card_3.png"
                },
                new Game
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567804"),
                    Title = "Shadow Ops",
                    Description = "Lead an elite black-ops unit through 20 covert missions across the globe. Plan your approach, utilize stealth or go loud, and manage your squad's unique abilities in this tactical stealth-action thriller.",
                    Price = 29.99m,
                    Genre = "Stealth",
                    ReleaseDate = new DateTime(2025, 8, 5),
                    Developer = "Shadow Works",
                    CoverImageUrl = "/images/game_card_4.png"
                },
                new Game
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567805"),
                    Title = "Dark Descent",
                    Description = "Descend into procedurally generated dungeons filled with relentless enemies and legendary loot. This hardcore action-RPG challenges you to master tight combat mechanics and build unstoppable character synergies.",
                    Price = 44.99m,
                    Genre = "Action RPG",
                    ReleaseDate = new DateTime(2025, 6, 18),
                    Developer = "Abyss Studios",
                    CoverImageUrl = "/images/game_card_5.png"
                },
                new Game
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567806"),
                    Title = "Turbo Drift X",
                    Description = "Hit the neon-lit streets in the ultimate arcade racing experience. Customize 50+ licensed vehicles, drift through iconic city circuits, and compete in online championships with players worldwide.",
                    Price = 34.99m,
                    Genre = "Racing",
                    ReleaseDate = new DateTime(2026, 2, 14),
                    Developer = "Speed Studios",
                    CoverImageUrl = "/images/game_card_6.png"
                },
                new Game
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567807"),
                    Title = "Stellar Voyage",
                    Description = "Chart your course through an infinite procedurally generated galaxy. Mine asteroids, trade exotic goods, build space stations, and discover alien civilizations in this relaxing yet deep space exploration simulator.",
                    Price = 54.99m,
                    Genre = "Exploration",
                    ReleaseDate = new DateTime(2025, 12, 1),
                    Developer = "Cosmos Labs",
                    CoverImageUrl = "/images/game_card_7.png"
                },
                new Game
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567808"),
                    Title = "Gate Protocol",
                    Description = "Humanity's first contact with alien intelligence has gone wrong. As a military scientist, use experimental portal technology to hop between dimensions, solve reality-bending puzzles, and prevent a multiverse collapse.",
                    Price = 24.99m,
                    Genre = "Sci-Fi",
                    ReleaseDate = new DateTime(2026, 5, 30),
                    Developer = "Protocol Games",
                    CoverImageUrl = "/images/game_card_8.png"
                }
            );
        }
    }
}
