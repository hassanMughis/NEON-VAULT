using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Neon_vault.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567801"),
                column: "AdditionalImageUrls",
                value: "/images/game_card_2.png,/images/game_card_3.png,/images/game_card_4.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567802"),
                column: "AdditionalImageUrls",
                value: "/images/game_card_3.png,/images/game_card_4.png,/images/game_card_5.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567803"),
                column: "AdditionalImageUrls",
                value: "/images/game_card_4.png,/images/game_card_5.png,/images/game_card_6.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567804"),
                column: "AdditionalImageUrls",
                value: "/images/game_card_5.png,/images/game_card_6.png,/images/game_card_7.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567805"),
                column: "AdditionalImageUrls",
                value: "/images/game_card_6.png,/images/game_card_7.png,/images/game_card_8.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567806"),
                column: "AdditionalImageUrls",
                value: "/images/game_card_7.png,/images/game_card_8.png,/images/game_card_1.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567807"),
                column: "AdditionalImageUrls",
                value: "/images/game_card_8.png,/images/game_card_1.png,/images/game_card_2.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567808"),
                column: "AdditionalImageUrls",
                value: "/images/game_card_1.png,/images/game_card_2.png,/images/game_card_3.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567801"),
                column: "AdditionalImageUrls",
                value: "/images/hardware_2.png,/images/hardware_3.png,/images/hardware_4.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567802"),
                column: "AdditionalImageUrls",
                value: "/images/hardware_3.png,/images/hardware_4.png,/images/hardware_1.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567803"),
                column: "AdditionalImageUrls",
                value: "/images/hardware_4.png,/images/hardware_1.png,/images/hardware_2.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567804"),
                columns: new[] { "AdditionalImageUrls", "Genre" },
                values: new object[] { "/images/hardware_1.png,/images/hardware_2.png,/images/hardware_3.png", "GPU" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AdditionalImageUrls", "Category", "CoverImageUrl", "Description", "Developer", "Genre", "Price", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567805"), "/images/hardware_2.png,/images/hardware_3.png,/images/hardware_4.png", "Hardware", "/images/hardware_1.png", "Supercharged DDR5 gaming RAM with premium heatsink and RGB lighting.", "NeonForge Hardware", "RAM", 129.99m, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Viper Neon DDR5 RAM 32GB" },
                    { new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567806"), "/images/hardware_3.png,/images/hardware_4.png,/images/hardware_1.png", "Hardware", "/images/hardware_2.png", "High-end motherboard featuring advanced cooling, PCIe 5.0, and dynamic lighting.", "NeonForge Hardware", "Motherboard", 389.99m, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ROG Matrix Z890 Motherboard" },
                    { new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567807"), "/images/hardware_4.png,/images/hardware_1.png,/images/hardware_2.png", "Hardware", "/images/hardware_3.png", "Blazing fast NVMe SSD with speeds up to 7400MB/s for instant load times.", "NeonForge Hardware", "SSD", 189.99m, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apex Quantum 2TB NVMe SSD" },
                    { new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567808"), "/images/hardware_1.png,/images/hardware_2.png,/images/hardware_3.png", "Hardware", "/images/hardware_4.png", "Next-gen multi-core processor for superior gaming performance and multitasking.", "NeonForge Hardware", "CPU", 549.99m, new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intel Core Ultra 9 Processor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567805"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567806"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567807"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567808"));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567801"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567802"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567803"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567804"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567805"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567806"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567807"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567808"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567801"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567802"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567803"),
                column: "AdditionalImageUrls",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7890-abcd-ef1234567804"),
                columns: new[] { "AdditionalImageUrls", "Genre" },
                values: new object[] { "", "Component" });
        }
    }
}
