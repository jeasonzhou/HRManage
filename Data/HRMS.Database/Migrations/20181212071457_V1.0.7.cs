using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Database.Migrations
{
    public partial class V107 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EmployeeEntry");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EmployeeDimission");

            migrationBuilder.DropColumn(
                name: "DimmissionType",
                table: "EmployeeDimission");

            migrationBuilder.RenameColumn(
                name: "DimmissionTime",
                table: "EmployeeDimission",
                newName: "DimissionTime");

            migrationBuilder.AddColumn<string>(
                name: "DimissionType",
                table: "EmployeeDimission",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employee",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DimissionType",
                table: "EmployeeDimission");

            migrationBuilder.RenameColumn(
                name: "DimissionTime",
                table: "EmployeeDimission",
                newName: "DimmissionTime");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EmployeeEntry",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EmployeeDimission",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DimmissionType",
                table: "EmployeeDimission",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employee",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
