using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "organization",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 15, 9, 8, 59, 295, DateTimeKind.Utc).AddTicks(5734));

            migrationBuilder.UpdateData(
                table: "organization",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 15, 9, 8, 59, 295, DateTimeKind.Utc).AddTicks(5759));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 15, 9, 8, 59, 295, DateTimeKind.Utc).AddTicks(6091));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "organization",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 15, 8, 32, 39, 561, DateTimeKind.Utc).AddTicks(7956));

            migrationBuilder.UpdateData(
                table: "organization",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 15, 8, 32, 39, 561, DateTimeKind.Utc).AddTicks(7981));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 15, 8, 32, 39, 561, DateTimeKind.Utc).AddTicks(8304));
        }
    }
}
