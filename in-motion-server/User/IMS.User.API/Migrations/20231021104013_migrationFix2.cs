using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.User.API.Migrations
{
    /// <inheritdoc />
    public partial class migrationFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_metas_user_profile_videos_profile_video_id",
                schema: "user",
                table: "user_metas");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_user_profile_videos_UserMetasId",
                schema: "user",
                table: "user_profile_videos");

            migrationBuilder.AlterColumn<Guid>(
                name: "profile_video_id",
                schema: "user",
                table: "user_metas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_metas_user_profile_videos_profile_video_id",
                schema: "user",
                table: "user_metas");

            migrationBuilder.AlterColumn<Guid>(
                name: "profile_video_id",
                schema: "user",
                table: "user_metas",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_user_profile_videos_UserMetasId",
                schema: "user",
                table: "user_profile_videos",
                column: "UserMetasId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_metas_user_profile_videos_profile_video_id",
                schema: "user",
                table: "user_metas",
                column: "profile_video_id",
                principalSchema: "user",
                principalTable: "user_profile_videos",
                principalColumn: "UserMetasId");
        }
    }
}
