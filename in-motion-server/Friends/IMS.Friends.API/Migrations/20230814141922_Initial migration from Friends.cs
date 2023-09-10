using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Friends.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialmigrationfromFriends : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "friends");

            migrationBuilder.CreateTable(
                name: "friendships",
                schema: "friends",
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_friendships_Id",
                schema: "friends",
                table: "friendships",
                column: "Id")
                .Annotation("Npgsql:IndexInclude", new[] { "second_user_id", "first_user_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friendships",
                schema: "friends");
        }
    }
}
