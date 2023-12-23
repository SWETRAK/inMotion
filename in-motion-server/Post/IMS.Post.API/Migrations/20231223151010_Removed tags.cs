using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Post.API.Migrations
{
    /// <inheritdoc />
    public partial class Removedtags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "posts_tags_relations",
                schema: "post");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "post");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tags",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    external_author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
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
    }
}
