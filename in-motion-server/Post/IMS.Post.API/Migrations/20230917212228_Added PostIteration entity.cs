using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Post.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedPostIterationentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "iteration_id",
                schema: "post",
                table: "posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "post_iterations",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_iterations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_iteration_id",
                schema: "post",
                table: "posts",
                column: "iteration_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_iterations_Id",
                schema: "post",
                table: "post_iterations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_post_iterations_iteration_id",
                schema: "post",
                table: "posts",
                column: "iteration_id",
                principalSchema: "post",
                principalTable: "post_iterations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_post_iterations_iteration_id",
                schema: "post",
                table: "posts");

            migrationBuilder.DropTable(
                name: "post_iterations",
                schema: "post");

            migrationBuilder.DropIndex(
                name: "IX_posts_iteration_id",
                schema: "post",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "iteration_id",
                schema: "post",
                table: "posts");
        }
    }
}
