using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Fixedrelationexception : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_user_profile_videos_profile_video_id",
                table: "users");

            migrationBuilder.AlterColumn<Guid>(
                name: "profile_video_id",
                table: "users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_profile_videos_profile_video_id",
                table: "users",
                column: "profile_video_id",
                principalTable: "user_profile_videos",
                principalColumn: "author_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_user_profile_videos_profile_video_id",
                table: "users");

            migrationBuilder.AlterColumn<Guid>(
                name: "profile_video_id",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_profile_videos_profile_video_id",
                table: "users",
                column: "profile_video_id",
                principalTable: "user_profile_videos",
                principalColumn: "author_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
