using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addedreactionswithconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReaction_posts_PostId",
                table: "PostReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReaction_users_AuthorId",
                table: "PostReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileVideoReaction_user_profile_videos_UserProfileVid~",
                table: "UserProfileVideoReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileVideoReaction_users_AuthorId",
                table: "UserProfileVideoReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfileVideoReaction",
                table: "UserProfileVideoReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostReaction",
                table: "PostReaction");

            migrationBuilder.RenameTable(
                name: "UserProfileVideoReaction",
                newName: "user_profile_video_reactions");

            migrationBuilder.RenameTable(
                name: "PostReaction",
                newName: "post_reactions");

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

            migrationBuilder.RenameColumn(
                name: "Emoji",
                table: "post_reactions",
                newName: "emoji");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "post_reactions",
                newName: "post_id");

            migrationBuilder.RenameColumn(
                name: "LastModificationDate",
                table: "post_reactions",
                newName: "last_modification_date");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "post_reactions",
                newName: "creation_date");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "post_reactions",
                newName: "author_id");

            migrationBuilder.RenameIndex(
                name: "IX_PostReaction_PostId",
                table: "post_reactions",
                newName: "IX_post_reactions_post_id");

            migrationBuilder.RenameIndex(
                name: "IX_PostReaction_AuthorId",
                table: "post_reactions",
                newName: "IX_post_reactions_author_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(760),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 873, DateTimeKind.Utc).AddTicks(8960));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(410),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 873, DateTimeKind.Utc).AddTicks(8680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "tags",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(7560),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 874, DateTimeKind.Utc).AddTicks(740));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 439, DateTimeKind.Utc).AddTicks(1030),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 874, DateTimeKind.Utc).AddTicks(3650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 439, DateTimeKind.Utc).AddTicks(670),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 874, DateTimeKind.Utc).AddTicks(3350));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 445, DateTimeKind.Utc).AddTicks(9740),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 879, DateTimeKind.Utc).AddTicks(9770));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 445, DateTimeKind.Utc).AddTicks(9320),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 879, DateTimeKind.Utc).AddTicks(9460));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(1740),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 880, DateTimeKind.Utc).AddTicks(1300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(1380),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 880, DateTimeKind.Utc).AddTicks(1020));

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modification_date",
                table: "user_profile_video_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(2490),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_video_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(2130),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "emoji",
                table: "post_reactions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modification_date",
                table: "post_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(3260),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(2910),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_profile_video_reactions",
                table: "user_profile_video_reactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_post_reactions",
                table: "post_reactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profile_video_reactions_Id",
                table: "user_profile_video_reactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_reactions_Id",
                table: "post_reactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_post_reactions_posts_post_id",
                table: "post_reactions",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_reactions_users_author_id",
                table: "post_reactions",
                column: "author_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_reactions_posts_post_id",
                table: "post_reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_post_reactions_users_author_id",
                table: "post_reactions");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_post_reactions",
                table: "post_reactions");

            migrationBuilder.DropIndex(
                name: "IX_post_reactions_Id",
                table: "post_reactions");

            migrationBuilder.RenameTable(
                name: "user_profile_video_reactions",
                newName: "UserProfileVideoReaction");

            migrationBuilder.RenameTable(
                name: "post_reactions",
                newName: "PostReaction");

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

            migrationBuilder.RenameColumn(
                name: "emoji",
                table: "PostReaction",
                newName: "Emoji");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "PostReaction",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "last_modification_date",
                table: "PostReaction",
                newName: "LastModificationDate");

            migrationBuilder.RenameColumn(
                name: "creation_date",
                table: "PostReaction",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "PostReaction",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_post_reactions_post_id",
                table: "PostReaction",
                newName: "IX_PostReaction_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_post_reactions_author_id",
                table: "PostReaction",
                newName: "IX_PostReaction_AuthorId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 873, DateTimeKind.Utc).AddTicks(8960),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 873, DateTimeKind.Utc).AddTicks(8680),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "tags",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 874, DateTimeKind.Utc).AddTicks(740),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(7560));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 874, DateTimeKind.Utc).AddTicks(3650),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 439, DateTimeKind.Utc).AddTicks(1030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 874, DateTimeKind.Utc).AddTicks(3350),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 439, DateTimeKind.Utc).AddTicks(670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 879, DateTimeKind.Utc).AddTicks(9770),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 445, DateTimeKind.Utc).AddTicks(9740));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 879, DateTimeKind.Utc).AddTicks(9460),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 445, DateTimeKind.Utc).AddTicks(9320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 880, DateTimeKind.Utc).AddTicks(1300),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(1740));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 43, 5, 880, DateTimeKind.Utc).AddTicks(1020),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(1380));

            migrationBuilder.AlterColumn<string>(
                name: "Emoji",
                table: "UserProfileVideoReaction",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationDate",
                table: "UserProfileVideoReaction",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(2490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "UserProfileVideoReaction",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(2130));

            migrationBuilder.AlterColumn<string>(
                name: "Emoji",
                table: "PostReaction",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationDate",
                table: "PostReaction",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(3260));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "PostReaction",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(2910));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfileVideoReaction",
                table: "UserProfileVideoReaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostReaction",
                table: "PostReaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReaction_posts_PostId",
                table: "PostReaction",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReaction_users_AuthorId",
                table: "PostReaction",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
