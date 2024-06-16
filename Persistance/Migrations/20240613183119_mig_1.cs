using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationID",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_OrganizationID",
                table: "Staffs",
                column: "OrganizationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Organizations_OrganizationID",
                table: "Staffs",
                column: "OrganizationID",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Organizations_OrganizationID",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_OrganizationID",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "OrganizationID",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");
        }
    }
}
