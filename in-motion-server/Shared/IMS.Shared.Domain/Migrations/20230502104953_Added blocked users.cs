using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addedblockedusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blocker_user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    second_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blocker_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_blocker_user_users_first_user_id",
                        column: x => x.first_user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_blocker_user_users_second_user_id",
                        column: x => x.second_user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blocker_user_first_user_id",
                table: "blocker_user",
                column: "first_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_blocker_user_Id",
                table: "blocker_user",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_blocker_user_second_user_id",
                table: "blocker_user",
                column: "second_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blocker_user");
        }
    }
}
