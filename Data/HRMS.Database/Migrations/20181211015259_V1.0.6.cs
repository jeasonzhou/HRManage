using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Database.Migrations
{
    public partial class V106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    FileName = table.Column<string>(maxLength: 200, nullable: true),
                    FileExtension = table.Column<string>(maxLength: 50, nullable: true),
                    PhysicAddr = table.Column<string>(maxLength: 500, nullable: true),
                    Url = table.Column<string>(maxLength: 500, nullable: true),
                    CreateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");
        }
    }
}
