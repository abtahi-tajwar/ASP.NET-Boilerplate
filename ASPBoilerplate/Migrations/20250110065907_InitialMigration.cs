using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPBoilerplate.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatHubConnections",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ConnectionId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatHubConnections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    OriginalName = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Storage = table.Column<int>(type: "INTEGER", nullable: false),
                    IsUsed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestrictedUserOtps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Otp = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestrictedUserOtps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnrestrictedUserOtps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Otp = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnrestrictedUserOtps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestrictedUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    IsEmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPasswordSet = table.Column<bool>(type: "INTEGER", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfileId = table.Column<string>(type: "TEXT", nullable: true),
                    OtpId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestrictedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestrictedUsers_RestrictedUserOtps_OtpId",
                        column: x => x.OtpId,
                        principalTable: "RestrictedUserOtps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RestrictedUsers_RestrictedUsers_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "RestrictedUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnrestrictedUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileId = table.Column<string>(type: "TEXT", nullable: true),
                    OtpId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnrestrictedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnrestrictedUsers_UnrestrictedUserOtps_OtpId",
                        column: x => x.OtpId,
                        principalTable: "UnrestrictedUserOtps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnrestrictedUsers_UnrestrictedUsers_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "UnrestrictedUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RestrictedUserProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DOB = table.Column<DateOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestrictedUserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestrictedUserProfiles_RestrictedUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RestrictedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestrictedUserTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceSignature = table.Column<string>(type: "TEXT", nullable: true),
                    Expiration = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RestrictedUserEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestrictedUserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestrictedUserTokens_RestrictedUsers_RestrictedUserEntityId",
                        column: x => x.RestrictedUserEntityId,
                        principalTable: "RestrictedUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnrestrictedUserProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DOB = table.Column<DateOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnrestrictedUserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnrestrictedUserProfiles_UnrestrictedUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "UnrestrictedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnrestrictedUserTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Otp = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceSignature = table.Column<string>(type: "TEXT", nullable: true),
                    Expiration = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnrestrictedUserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnrestrictedUserTokens_UnrestrictedUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "UnrestrictedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestrictedUserProfiles_UserId",
                table: "RestrictedUserProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RestrictedUsers_Email",
                table: "RestrictedUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestrictedUsers_OtpId",
                table: "RestrictedUsers",
                column: "OtpId");

            migrationBuilder.CreateIndex(
                name: "IX_RestrictedUsers_ProfileId",
                table: "RestrictedUsers",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_RestrictedUserTokens_RestrictedUserEntityId",
                table: "RestrictedUserTokens",
                column: "RestrictedUserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UnrestrictedUserProfiles_UserId",
                table: "UnrestrictedUserProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnrestrictedUsers_OtpId",
                table: "UnrestrictedUsers",
                column: "OtpId");

            migrationBuilder.CreateIndex(
                name: "IX_UnrestrictedUsers_ProfileId",
                table: "UnrestrictedUsers",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UnrestrictedUserTokens_UserId",
                table: "UnrestrictedUserTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatHubConnections");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "RestrictedUserProfiles");

            migrationBuilder.DropTable(
                name: "RestrictedUserTokens");

            migrationBuilder.DropTable(
                name: "UnrestrictedUserProfiles");

            migrationBuilder.DropTable(
                name: "UnrestrictedUserTokens");

            migrationBuilder.DropTable(
                name: "RestrictedUsers");

            migrationBuilder.DropTable(
                name: "UnrestrictedUsers");

            migrationBuilder.DropTable(
                name: "RestrictedUserOtps");

            migrationBuilder.DropTable(
                name: "UnrestrictedUserOtps");
        }
    }
}
