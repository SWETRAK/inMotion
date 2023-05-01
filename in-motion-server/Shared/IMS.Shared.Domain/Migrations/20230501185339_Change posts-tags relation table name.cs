using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Changepoststagsrelationtablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_posts_PostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_tags_TagsId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "posts_tags_relations");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagsId",
                table: "posts_tags_relations",
                newName: "IX_posts_tags_relations_TagsId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 214, DateTimeKind.Utc).AddTicks(8920),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 214, DateTimeKind.Utc).AddTicks(8650),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modification_date",
                table: "user_profile_video_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(390),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(2490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_video_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(100),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(2130));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "tags",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(4920),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(7560));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(7720),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 439, DateTimeKind.Utc).AddTicks(1030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(7430),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 439, DateTimeKind.Utc).AddTicks(670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(5400),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 445, DateTimeKind.Utc).AddTicks(9740));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(5080),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 445, DateTimeKind.Utc).AddTicks(9320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modification_date",
                table: "post_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(8320),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(3260));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(8030),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(2910));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(6980),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(1740));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(6690),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(1380));

            migrationBuilder.AddPrimaryKey(
                name: "PK_posts_tags_relations",
                table: "posts_tags_relations",
                columns: new[] { "PostId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_posts_tags_relations_posts_PostId",
                table: "posts_tags_relations",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_tags_relations_tags_TagsId",
                table: "posts_tags_relations",
                column: "TagsId",
                principalTable: "tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_tags_relations_posts_PostId",
                table: "posts_tags_relations");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_tags_relations_tags_TagsId",
                table: "posts_tags_relations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_posts_tags_relations",
                table: "posts_tags_relations");

            migrationBuilder.RenameTable(
                name: "posts_tags_relations",
                newName: "PostTag");

            migrationBuilder.RenameIndex(
                name: "IX_posts_tags_relations_TagsId",
                table: "PostTag",
                newName: "IX_PostTag_TagsId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(760),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 214, DateTimeKind.Utc).AddTicks(8920));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(410),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 214, DateTimeKind.Utc).AddTicks(8650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modification_date",
                table: "user_profile_video_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(2490),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_video_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(2130),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(100));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "tags",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 438, DateTimeKind.Utc).AddTicks(7560),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(4920));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 439, DateTimeKind.Utc).AddTicks(1030),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(7720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 439, DateTimeKind.Utc).AddTicks(670),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(7430));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 445, DateTimeKind.Utc).AddTicks(9740),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(5400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 445, DateTimeKind.Utc).AddTicks(9320),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(5080));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modification_date",
                table: "post_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(3260),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(2910),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(8030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(1740),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(6980));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 50, 21, 446, DateTimeKind.Utc).AddTicks(1380),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag",
                columns: new[] { "PostId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_posts_PostId",
                table: "PostTag",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_tags_TagsId",
                table: "PostTag",
                column: "TagsId",
                principalTable: "tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
