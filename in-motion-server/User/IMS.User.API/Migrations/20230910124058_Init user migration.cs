using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.User.API.Migrations
{
    /// <inheritdoc />
    public partial class Initusermigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.CreateTable(
                name: "user_profile_videos",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserMetasId = table.Column<Guid>(type: "uuid", nullable: false),
                    filename = table.Column<string>(type: "text", nullable: false),
                    bucket_location = table.Column<string>(type: "text", nullable: false),
                    bucket_name = table.Column<string>(type: "text", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_edition_name = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profile_videos", x => x.id);
                    table.UniqueConstraint("AK_user_profile_videos_UserMetasId", x => x.UserMetasId);
                });

            migrationBuilder.CreateTable(
                name: "user_metas",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserExternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    bio = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    profile_video_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_metas", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_metas_user_profile_videos_profile_video_id",
                        column: x => x.profile_video_id,
                        principalSchema: "user",
                        principalTable: "user_profile_videos",
                        principalColumn: "UserMetasId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_metas_id",
                schema: "user",
                table: "user_metas",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_metas_profile_video_id",
                schema: "user",
                table: "user_metas",
                column: "profile_video_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_profile_videos_id",
                schema: "user",
                table: "user_profile_videos",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_metas",
                schema: "user");

            migrationBuilder.DropTable(
                name: "user_profile_videos",
                schema: "user");
        }
    }
}
