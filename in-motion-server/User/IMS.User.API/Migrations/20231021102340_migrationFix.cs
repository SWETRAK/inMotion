using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.User.API.Migrations
{
    /// <inheritdoc />
    public partial class migrationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_profile_videos_id",
                schema: "user",
                table: "user_profile_videos");

            migrationBuilder.DropIndex(
                name: "IX_user_metas_id",
                schema: "user",
                table: "user_metas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_user_profile_videos_id",
                schema: "user",
                table: "user_profile_videos",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_metas_id",
                schema: "user",
                table: "user_metas",
                column: "id");
        }
    }
}
