using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.User.API.Migrations
{
    /// <inheritdoc />
    public partial class migrationFix6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_user_metas_user_profile_videos_profile_video_id",
                schema: "user",
                table: "user_metas",
                column: "profile_video_id",
                principalSchema: "user",
                principalTable: "user_profile_videos",
                principalColumn: "id");
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
    }
}
