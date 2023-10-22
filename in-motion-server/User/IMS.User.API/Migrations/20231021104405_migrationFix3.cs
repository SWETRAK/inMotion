using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.User.API.Migrations
{
    /// <inheritdoc />
    public partial class migrationFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_metas_user_profile_videos_profile_video_id",
                schema: "user",
                table: "user_metas");

            migrationBuilder.DropIndex(
                name: "IX_user_metas_profile_video_id",
                schema: "user",
                table: "user_metas");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_user_metas_profile_video_id",
                schema: "user",
                table: "user_metas",
                column: "profile_video_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profile_videos_UserMetasId",
                schema: "user",
                table: "user_profile_videos",
                column: "UserMetasId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_user_profile_videos_user_metas_UserMetasId",
                schema: "user",
                table: "user_profile_videos",
                column: "UserMetasId",
                principalSchema: "user",
                principalTable: "user_metas",
                principalColumn: "profile_video_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_profile_videos_user_metas_UserMetasId",
                schema: "user",
                table: "user_profile_videos");

            migrationBuilder.DropIndex(
                name: "IX_user_profile_videos_UserMetasId",
                schema: "user",
                table: "user_profile_videos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_user_metas_profile_video_id",
                schema: "user",
                table: "user_metas");

            migrationBuilder.CreateIndex(
                name: "IX_user_metas_profile_video_id",
                schema: "user",
                table: "user_metas",
                column: "profile_video_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_user_metas_user_profile_videos_profile_video_id",
                schema: "user",
                table: "user_metas",
                column: "profile_video_id",
                principalSchema: "user",
                principalTable: "user_profile_videos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
