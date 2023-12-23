using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Post.API.Migrations
{
    /// <inheritdoc />
    public partial class Removedpostcommentreaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_comment_reaction",
                schema: "post");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "post_comment_reaction",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    post_comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    emoji = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    external_author_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "IX_post_comment_reaction_Id",
                schema: "post",
                table: "post_comment_reaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_reaction_post_comment_id",
                schema: "post",
                table: "post_comment_reaction",
                column: "post_comment_id");
        }
    }
}
