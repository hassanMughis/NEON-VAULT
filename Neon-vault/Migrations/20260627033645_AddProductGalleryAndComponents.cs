using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neon_vault.Migrations
{
    /// <inheritdoc />
    public partial class AddProductGalleryAndComponents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalImageUrls",
                table: "Games",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

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
                column: "AdditionalImageUrls",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalImageUrls",
                table: "Games");
        }
    }
}
