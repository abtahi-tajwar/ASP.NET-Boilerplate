using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ASPBoilerplate.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "CreatedAt", "IsUsed", "Location", "Name", "Storage", "UpdatedAt" },
                values: new object[,]
                {
                    { "1", new DateTime(2024, 12, 24, 18, 37, 42, 786, DateTimeKind.Local).AddTicks(2470), true, "file1.txt", "File 1", 0, new DateTime(2024, 12, 24, 18, 37, 42, 794, DateTimeKind.Local).AddTicks(4580) },
                    { "2", new DateTime(2024, 12, 24, 18, 37, 42, 794, DateTimeKind.Local).AddTicks(4720), true, "file2.txt", "File 2", 0, new DateTime(2024, 12, 24, 18, 37, 42, 794, DateTimeKind.Local).AddTicks(4720) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
