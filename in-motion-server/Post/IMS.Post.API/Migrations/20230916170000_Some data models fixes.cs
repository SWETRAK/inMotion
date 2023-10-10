using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Post.API.Migrations
{
    /// <inheritdoc />
    public partial class Somedatamodelsfixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "post");

            migrationBuilder.CreateTable(
                name: "localizations",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    visibility = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    localization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_posts_localizations_localization_id",
                        column: x => x.localization_id,
                        principalSchema: "post",
                        principalTable: "localizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_comments",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_comments_posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "post",
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_reactions",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    emoji = table.Column<string>(type: "text", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_reactions_posts_post_id",
                        column: x => x.post_id,
                        principalSchema: "post",
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_videos",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    filename = table.Column<string>(type: "text", nullable: false),
                    bucket_location = table.Column<string>(type: "text", nullable: false),
                    bucket_name = table.Column<string>(type: "text", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_edition_name = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_videos_posts_post_id",
                        column: x => x.post_id,
                        principalSchema: "post",
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "posts_tags_relations",
                schema: "post",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts_tags_relations", x => new { x.PostId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_posts_tags_relations_posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "post",
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_posts_tags_relations_tags_TagsId",
                        column: x => x.TagsId,
                        principalSchema: "post",
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_comment_reaction",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    emoji = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    post_comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_comment_reaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_comment_reaction_post_comments_post_comment_id",
                        column: x => x.post_comment_id,
                        principalSchema: "post",
                        principalTable: "post_comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_localizations_Id",
                schema: "post",
                table: "localizations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_reaction_Id",
                schema: "post",
                table: "post_comment_reaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_reaction_post_comment_id",
                schema: "post",
                table: "post_comment_reaction",
                column: "post_comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_comments_Id",
                schema: "post",
                table: "post_comments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_comments_PostId",
                schema: "post",
                table: "post_comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_post_reactions_Id",
                schema: "post",
                table: "post_reactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_reactions_post_id",
                schema: "post",
                table: "post_reactions",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_videos_Id",
                schema: "post",
                table: "post_videos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_videos_post_id",
                schema: "post",
                table: "post_videos",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_posts_Id",
                schema: "post",
                table: "posts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_posts_localization_id",
                schema: "post",
                table: "posts",
                column: "localization_id");

            migrationBuilder.CreateIndex(
                name: "IX_posts_tags_relations_TagsId",
                schema: "post",
                table: "posts_tags_relations",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_tags_Id",
                schema: "post",
                table: "tags",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_comment_reaction",
                schema: "post");

            migrationBuilder.DropTable(
                name: "post_reactions",
                schema: "post");

            migrationBuilder.DropTable(
                name: "post_videos",
                schema: "post");

            migrationBuilder.DropTable(
                name: "posts_tags_relations",
                schema: "post");

            migrationBuilder.DropTable(
                name: "post_comments",
                schema: "post");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "post");

            migrationBuilder.DropTable(
                name: "posts",
                schema: "post");

            migrationBuilder.DropTable(
                name: "localizations",
                schema: "post");
        }
    }
}
