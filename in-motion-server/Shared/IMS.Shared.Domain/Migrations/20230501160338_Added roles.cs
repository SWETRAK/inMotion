using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addedroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "user_profile_video",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 577, DateTimeKind.Utc).AddTicks(7450),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(1330));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_video",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 577, DateTimeKind.Utc).AddTicks(7050),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(1050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "tag",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 577, DateTimeKind.Utc).AddTicks(8690),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(2450));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 600, DateTimeKind.Utc).AddTicks(5610),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(6120));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 600, DateTimeKind.Utc).AddTicks(5210),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(5780));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "post_comment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 600, DateTimeKind.Utc).AddTicks(7020),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(7540));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_comment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 600, DateTimeKind.Utc).AddTicks(6700),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(7230));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "user_profile_video",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(1330),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 577, DateTimeKind.Utc).AddTicks(7450));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_video",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(1050),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 577, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "tag",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(2450),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 577, DateTimeKind.Utc).AddTicks(8690));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(6120),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 600, DateTimeKind.Utc).AddTicks(5610));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(5780),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 600, DateTimeKind.Utc).AddTicks(5210));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "post_comment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(7540),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 600, DateTimeKind.Utc).AddTicks(7020));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_comment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(7230),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 16, 3, 38, 600, DateTimeKind.Utc).AddTicks(6700));
        }
    }
}
