using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fullname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_staff_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "organization",
                columns: new[] { "Id", "active", "created_at", "name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 7, 15, 8, 32, 39, 561, DateTimeKind.Utc).AddTicks(7956), "ABB" },
                    { 2, false, new DateTime(2024, 7, 15, 8, 32, 39, 561, DateTimeKind.Utc).AddTicks(7981), "IDDA" }
                });

            migrationBuilder.InsertData(
                table: "staff",
                columns: new[] { "Id", "active", "created_at", "fullname", "organization_id", "username" },
                values: new object[] { 1, null, new DateTime(2024, 7, 15, 8, 32, 39, 561, DateTimeKind.Utc).AddTicks(8304), "ilkin suleymanov ", 1, "ilkinsuleymanov200@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_organization_name",
                table: "organization",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_staff_organization_id",
                table: "staff",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_username",
                table: "staff",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "organization");
        }
    }
}
