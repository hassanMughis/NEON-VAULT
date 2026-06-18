using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Neon_vault.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibraryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryItems_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceAtPurchase = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "CoverImageUrl", "Description", "Developer", "Genre", "Price", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567801"), "/images/game_card_1.png", "Dive into a sprawling cyberpunk metropolis as a rogue hacker with mysterious powers. Uncover corporate conspiracies, upgrade your neural implants, and shape the fate of Neo Shanghai in this award-winning open-world RPG.", "NeonForge Studios", "RPG", 49.99m, new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neon Phantom" },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567802"), "/images/game_card_2.png", "Fast-paced multiplayer FPS set on abandoned space stations. Team up with friends or go solo in intense 60-player battles across zero-gravity arenas. Features advanced movement mechanics and destructible environments.", "Void Interactive", "FPS", 39.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Void Runners" },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567803"), "/images/game_card_3.png", "A breathtaking fantasy adventure through shattered realms where magic and technology collide. Master elemental powers, solve ancient puzzles, and battle mythical creatures in a world teetering on the edge of destruction.", "Rift Games", "Adventure", 59.99m, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arcane Rift" },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567804"), "/images/game_card_4.png", "Lead an elite black-ops unit through 20 covert missions across the globe. Plan your approach, utilize stealth or go loud, and manage your squad's unique abilities in this tactical stealth-action thriller.", "Shadow Works", "Stealth", 29.99m, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shadow Ops" },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567805"), "/images/game_card_5.png", "Descend into procedurally generated dungeons filled with relentless enemies and legendary loot. This hardcore action-RPG challenges you to master tight combat mechanics and build unstoppable character synergies.", "Abyss Studios", "Action RPG", 44.99m, new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dark Descent" },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567806"), "/images/game_card_6.png", "Hit the neon-lit streets in the ultimate arcade racing experience. Customize 50+ licensed vehicles, drift through iconic city circuits, and compete in online championships with players worldwide.", "Speed Studios", "Racing", 34.99m, new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turbo Drift X" },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567807"), "/images/game_card_7.png", "Chart your course through an infinite procedurally generated galaxy. Mine asteroids, trade exotic goods, build space stations, and discover alien civilizations in this relaxing yet deep space exploration simulator.", "Cosmos Labs", "Exploration", 54.99m, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stellar Voyage" },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567808"), "/images/game_card_8.png", "Humanity's first contact with alien intelligence has gone wrong. As a military scientist, use experimental portal technology to hop between dimensions, solve reality-bending puzzles, and prevent a multiverse collapse.", "Protocol Games", "Sci-Fi", 24.99m, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gate Protocol" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_GameId",
                table: "CartItems",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryItems_GameId",
                table: "LibraryItems",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_GameId",
                table: "OrderItems",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "LibraryItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
