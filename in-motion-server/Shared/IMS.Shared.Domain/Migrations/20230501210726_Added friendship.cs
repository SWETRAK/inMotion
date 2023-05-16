using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addedfriendship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "friendships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    second_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_friendships_users_first_user_id",
                        column: x => x.first_user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_friendships_users_second_user_id",
                        column: x => x.second_user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friendships_first_user_id",
                table: "friendships",
                column: "first_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_friendships_Id",
                table: "friendships",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_friendships_second_user_id",
                table: "friendships",
                column: "second_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friendships");
        }
    }
}
