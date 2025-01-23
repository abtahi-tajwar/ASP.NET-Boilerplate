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
                name: "ChatInboxes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    MessagedUserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatInboxes", x => x.Id);
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
                name: "MessageHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SenderId = table.Column<string>(type: "TEXT", nullable: false),
                    ReceiverId = table.Column<string>(type: "TEXT", nullable: false),
                    SentAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOtps",
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
                    table.PrimaryKey("PK_UserOtps", x => x.Id);
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
                    OtpId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestrictedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestrictedUsers_UserOtps_OtpId",
                        column: x => x.OtpId,
                        principalTable: "UserOtps",
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
                        name: "FK_UnrestrictedUsers_UnrestrictedUsers_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "UnrestrictedUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnrestrictedUsers_UserOtps_OtpId",
                        column: x => x.OtpId,
                        principalTable: "UserOtps",
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
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceSignature = table.Column<string>(type: "TEXT", nullable: true),
                    Expiration = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RestrictedUserEntityId = table.Column<string>(type: "TEXT", nullable: true),
                    UnrestrictedUserEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_RestrictedUsers_RestrictedUserEntityId",
                        column: x => x.RestrictedUserEntityId,
                        principalTable: "RestrictedUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTokens_UnrestrictedUsers_UnrestrictedUserEntityId",
                        column: x => x.UnrestrictedUserEntityId,
                        principalTable: "UnrestrictedUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestrictedUserProfiles_UserId",
                table: "RestrictedUserProfiles",
                column: "UserId",
                unique: true);

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
                name: "IX_UserTokens_RestrictedUserEntityId",
                table: "UserTokens",
                column: "RestrictedUserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UnrestrictedUserEntityId",
                table: "UserTokens",
                column: "UnrestrictedUserEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatHubConnections");

            migrationBuilder.DropTable(
                name: "ChatInboxes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "MessageHistories");

            migrationBuilder.DropTable(
                name: "RestrictedUserProfiles");

            migrationBuilder.DropTable(
                name: "UnrestrictedUserProfiles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "RestrictedUsers");

            migrationBuilder.DropTable(
                name: "UnrestrictedUsers");

            migrationBuilder.DropTable(
                name: "UserOtps");
        }
    }
}
