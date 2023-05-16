using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addedpaswordtouserentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "users");
        }
    }
}
