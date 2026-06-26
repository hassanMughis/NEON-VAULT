using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neon_vault.Migrations
{
    /// <inheritdoc />
    public partial class AddChatUserProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "ChatUsers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "ChatUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ChatUsers",
                type: "nvarchar(320)",
                maxLength: 320,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ChatUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "ChatUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "ChatUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "ChatUsers");
        }
    }
}
