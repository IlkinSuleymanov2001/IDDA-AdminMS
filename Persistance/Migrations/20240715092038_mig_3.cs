using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "organization",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 15, 9, 20, 36, 502, DateTimeKind.Utc).AddTicks(1405));

            migrationBuilder.UpdateData(
                table: "organization",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 15, 9, 20, 36, 502, DateTimeKind.Utc).AddTicks(1426));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "active", "created_at" },
                values: new object[] { true, new DateTime(2024, 7, 15, 9, 20, 36, 502, DateTimeKind.Utc).AddTicks(1749) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "active", "created_at" },
                values: new object[] { null, new DateTime(2024, 7, 15, 9, 8, 59, 295, DateTimeKind.Utc).AddTicks(6091) });
        }
    }
}
