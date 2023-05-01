using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "localization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_profile_video",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Filename = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 740, DateTimeKind.Utc).AddTicks(1500)),
                    last_edition_name = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 740, DateTimeKind.Utc).AddTicks(1900))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profile_video", x => x.Id);
                    table.UniqueConstraint("AK_user_profile_video_author_id", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nickname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Bio = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    profile_video_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_user_profile_video_profile_video_id",
                        column: x => x.profile_video_id,
                        principalTable: "user_profile_video",
                        principalColumn: "author_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_videos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    post_front_id = table.Column<Guid>(type: "uuid", nullable: false),
                    post_rear_id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    Filename = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(4980)),
                    last_edition_name = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(5350))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_videos", x => x.Id);
                    table.UniqueConstraint("AK_post_videos_post_front_id", x => x.post_front_id);
                    table.UniqueConstraint("AK_post_videos_post_rear_id", x => x.post_rear_id);
                    table.ForeignKey(
                        name: "FK_post_videos_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<int>(type: "integer", nullable: false),
                    auth_key = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 740, DateTimeKind.Utc).AddTicks(3060))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tag_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    localization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    front_video_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rear_video_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_localization_localization_id",
                        column: x => x.localization_id,
                        principalTable: "localization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_post_videos_front_video_id",
                        column: x => x.front_video_id,
                        principalTable: "post_videos",
                        principalColumn: "post_front_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_post_videos_rear_video_id",
                        column: x => x.rear_video_id,
                        principalTable: "post_videos",
                        principalColumn: "post_rear_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(6610)),
                    last_modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(6930))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_comment_post_PostId",
                        column: x => x.PostId,
                        principalTable: "post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_comment_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PostTag_post_PostId",
                        column: x => x.PostId,
                        principalTable: "post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_author_id",
                table: "post",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_front_video_id",
                table: "post",
                column: "front_video_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_post_localization_id",
                table: "post",
                column: "localization_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_rear_video_id",
                table: "post",
                column: "rear_video_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_author_id",
                table: "post_comment",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_PostId",
                table: "post_comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_post_videos_AuthorId",
                table: "post_videos",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagsId",
                table: "PostTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_user_id",
                table: "Providers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_author_id",
                table: "tag",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_Id",
                table: "users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_users_profile_video_id",
                table: "users",
                column: "profile_video_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_comment");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "localization");

            migrationBuilder.DropTable(
                name: "post_videos");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "user_profile_video");
        }
    }
}
