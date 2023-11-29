using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Post.API.Migrations
{
    /// <inheritdoc />
    public partial class Removedlocalizationentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_localizations_localization_id",
                schema: "post",
                table: "posts");

            migrationBuilder.DropTable(
                name: "localizations",
                schema: "post");

            migrationBuilder.DropIndex(
                name: "IX_posts_localization_id",
                schema: "post",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "localization_id",
                schema: "post",
                table: "posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "localization_id",
                schema: "post",
                table: "posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "localizations",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localizations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_localization_id",
                schema: "post",
                table: "posts",
                column: "localization_id");

            migrationBuilder.CreateIndex(
                name: "IX_localizations_Id",
                schema: "post",
                table: "localizations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_localizations_localization_id",
                schema: "post",
                table: "posts",
                column: "localization_id",
                principalSchema: "post",
                principalTable: "localizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
