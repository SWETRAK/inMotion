using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addedlocalhostaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "user_profile_video",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(1330),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 740, DateTimeKind.Utc).AddTicks(1900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_video",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(1050),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 740, DateTimeKind.Utc).AddTicks(1500));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "tag",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(2450),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 740, DateTimeKind.Utc).AddTicks(3060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(6120),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(5350));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(5780),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(4980));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "post_comment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(7540),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_comment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(7230),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(6610));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "user_profile_video",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 740, DateTimeKind.Utc).AddTicks(1900),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(1330));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "user_profile_video",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 740, DateTimeKind.Utc).AddTicks(1500),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(1050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "tag",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 740, DateTimeKind.Utc).AddTicks(3060),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 870, DateTimeKind.Utc).AddTicks(2450));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_edition_name",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(5350),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(6120));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_videos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(4980),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(5780));

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_date",
                table: "post_comment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(6930),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(7540));

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date",
                table: "post_comment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 15, 46, 44, 755, DateTimeKind.Utc).AddTicks(6610),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 1, 15, 50, 5, 883, DateTimeKind.Utc).AddTicks(7230));
        }
    }
}
