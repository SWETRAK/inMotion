using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addedcontenttypetovideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "user_profile_videos");

            migrationBuilder.DropColumn(
                name: "description",
                table: "post_videos");

            migrationBuilder.AddColumn<string>(
                name: "content_type",
                table: "user_profile_videos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "content_type",
                table: "post_videos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content_type",
                table: "user_profile_videos");

            migrationBuilder.DropColumn(
                name: "content_type",
                table: "post_videos");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "user_profile_videos",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "post_videos",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);
        }
    }
}
