using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Removeduserprofilevideoreactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_profile_video_reactions_user_profile_videos_user_profi~",
                table: "user_profile_video_reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_user_profile_video_reactions_users_author_id",
                table: "user_profile_video_reactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_profile_video_reactions",
                table: "user_profile_video_reactions");

            migrationBuilder.DropIndex(
                name: "IX_user_profile_video_reactions_Id",
                table: "user_profile_video_reactions");

            migrationBuilder.RenameTable(
                name: "user_profile_video_reactions",
                newName: "UserProfileVideoReaction");

            migrationBuilder.RenameColumn(
                name: "emoji",
                table: "UserProfileVideoReaction",
                newName: "Emoji");

            migrationBuilder.RenameColumn(
                name: "user_profile_video_id",
                table: "UserProfileVideoReaction",
                newName: "UserProfileVideoId");

            migrationBuilder.RenameColumn(
                name: "last_modification_date",
                table: "UserProfileVideoReaction",
                newName: "LastModificationDate");

            migrationBuilder.RenameColumn(
                name: "creation_date",
                table: "UserProfileVideoReaction",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "UserProfileVideoReaction",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_user_profile_video_reactions_user_profile_video_id",
                table: "UserProfileVideoReaction",
                newName: "IX_UserProfileVideoReaction_UserProfileVideoId");

            migrationBuilder.RenameIndex(
                name: "IX_user_profile_video_reactions_author_id",
                table: "UserProfileVideoReaction",
                newName: "IX_UserProfileVideoReaction_AuthorId");

            migrationBuilder.AlterColumn<string>(
                name: "Emoji",
                table: "UserProfileVideoReaction",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfileVideoReaction",
                table: "UserProfileVideoReaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileVideoReaction_user_profile_videos_UserProfileVid~",
                table: "UserProfileVideoReaction",
                column: "UserProfileVideoId",
                principalTable: "user_profile_videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileVideoReaction_users_AuthorId",
                table: "UserProfileVideoReaction",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileVideoReaction_user_profile_videos_UserProfileVid~",
                table: "UserProfileVideoReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileVideoReaction_users_AuthorId",
                table: "UserProfileVideoReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfileVideoReaction",
                table: "UserProfileVideoReaction");

            migrationBuilder.RenameTable(
                name: "UserProfileVideoReaction",
                newName: "user_profile_video_reactions");

            migrationBuilder.RenameColumn(
                name: "Emoji",
                table: "user_profile_video_reactions",
                newName: "emoji");

            migrationBuilder.RenameColumn(
                name: "UserProfileVideoId",
                table: "user_profile_video_reactions",
                newName: "user_profile_video_id");

            migrationBuilder.RenameColumn(
                name: "LastModificationDate",
                table: "user_profile_video_reactions",
                newName: "last_modification_date");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "user_profile_video_reactions",
                newName: "creation_date");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "user_profile_video_reactions",
                newName: "author_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfileVideoReaction_UserProfileVideoId",
                table: "user_profile_video_reactions",
                newName: "IX_user_profile_video_reactions_user_profile_video_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfileVideoReaction_AuthorId",
                table: "user_profile_video_reactions",
                newName: "IX_user_profile_video_reactions_author_id");

            migrationBuilder.AlterColumn<string>(
                name: "emoji",
                table: "user_profile_video_reactions",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_profile_video_reactions",
                table: "user_profile_video_reactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profile_video_reactions_Id",
                table: "user_profile_video_reactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_profile_video_reactions_user_profile_videos_user_profi~",
                table: "user_profile_video_reactions",
                column: "user_profile_video_id",
                principalTable: "user_profile_videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_profile_video_reactions_users_author_id",
                table: "user_profile_video_reactions",
                column: "author_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
