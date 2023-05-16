using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addedbucketconfigforvideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "users",
                newName: "nickname");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "users",
                newName: "bio");

            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "users",
                newName: "hashed_password");

            migrationBuilder.AddColumn<string>(
                name: "bucket_location",
                table: "user_profile_videos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "bucket_name",
                table: "user_profile_videos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "bucket_location",
                table: "post_videos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "bucket_name",
                table: "post_videos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bucket_location",
                table: "user_profile_videos");

            migrationBuilder.DropColumn(
                name: "bucket_name",
                table: "user_profile_videos");

            migrationBuilder.DropColumn(
                name: "bucket_location",
                table: "post_videos");

            migrationBuilder.DropColumn(
                name: "bucket_name",
                table: "post_videos");

            migrationBuilder.RenameColumn(
                name: "nickname",
                table: "users",
                newName: "Nickname");

            migrationBuilder.RenameColumn(
                name: "bio",
                table: "users",
                newName: "Bio");

            migrationBuilder.RenameColumn(
                name: "hashed_password",
                table: "users",
                newName: "HashedPassword");
        }
    }
}
