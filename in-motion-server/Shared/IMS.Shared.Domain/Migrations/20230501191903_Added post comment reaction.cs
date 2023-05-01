using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addedpostcommentreaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 516, DateTimeKind.Utc).AddTicks(2390),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 214, DateTimeKind.Utc).AddTicks(8920));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 516, DateTimeKind.Utc).AddTicks(2100),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 214, DateTimeKind.Utc).AddTicks(8650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modification_date",
                table: "user_profile_video_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 516, DateTimeKind.Utc).AddTicks(3840),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_video_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 516, DateTimeKind.Utc).AddTicks(3540),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(100));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "tags",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 516, DateTimeKind.Utc).AddTicks(7720),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(4920));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 517, DateTimeKind.Utc).AddTicks(530),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(7720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 517, DateTimeKind.Utc).AddTicks(230),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(7430));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(270),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(5400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 522, DateTimeKind.Utc).AddTicks(9960),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(5080));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modification_date",
                table: "post_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(3190),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(2890),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(8030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(1780),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(6980));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(1490),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.CreateTable(
                name: "post_comment_reaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    post_comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    emoji = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(6370)),
                    last_modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(6670))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_comment_reaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_comment_reaction_post_comments_post_comment_id",
                        column: x => x.post_comment_id,
                        principalTable: "post_comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_comment_reaction_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_reaction_author_id",
                table: "post_comment_reaction",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_reaction_Id",
                table: "post_comment_reaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_reaction_post_comment_id",
                table: "post_comment_reaction",
                column: "post_comment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_comment_reaction");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 214, DateTimeKind.Utc).AddTicks(8920),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 516, DateTimeKind.Utc).AddTicks(2390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 214, DateTimeKind.Utc).AddTicks(8650),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 516, DateTimeKind.Utc).AddTicks(2100));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modification_date",
                table: "user_profile_video_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(390),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 516, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_video_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(100),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 516, DateTimeKind.Utc).AddTicks(3540));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "tags",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(4920),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 516, DateTimeKind.Utc).AddTicks(7720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(7720),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 517, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 215, DateTimeKind.Utc).AddTicks(7430),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 517, DateTimeKind.Utc).AddTicks(230));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(5400),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(270));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(5080),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 522, DateTimeKind.Utc).AddTicks(9960));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modification_date",
                table: "post_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(8320),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(3190));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(8030),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(2890));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(6980),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(1780));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 18, 53, 39, 221, DateTimeKind.Utc).AddTicks(6690),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 19, 19, 3, 523, DateTimeKind.Utc).AddTicks(1490));
        }
    }
}
