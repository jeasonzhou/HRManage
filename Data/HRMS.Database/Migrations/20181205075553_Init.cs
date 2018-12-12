using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Database.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeName = table.Column<string>(maxLength: 150, nullable: false),
                    Department = table.Column<string>(maxLength: 150, nullable: false),
                    Position = table.Column<string>(maxLength: 150, nullable: false),
                    EmployeeType = table.Column<string>(maxLength: 4, nullable: false),
                    CheckTime = table.Column<DateTime>(nullable: false),
                    EquipmentCode = table.Column<string>(maxLength: 30, nullable: false),
                    CheckClass = table.Column<string>(maxLength: 4, nullable: true),
                    CheckType = table.Column<string>(maxLength: 4, nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    ShortName = table.Column<string>(maxLength: 150, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    RegisteredAddress = table.Column<string>(maxLength: 200, nullable: false),
                    Contact = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    MobilePhone = table.Column<string>(maxLength: 20, nullable: true),
                    Representative = table.Column<string>(maxLength: 100, nullable: false),
                    Establishment = table.Column<DateTime>(nullable: false),
                    OperationPeriod = table.Column<string>(maxLength: 20, nullable: true),
                    RegisteredCapital = table.Column<string>(maxLength: 20, nullable: true),
                    CreditCode = table.Column<string>(maxLength: 30, nullable: false),
                    Valid = table.Column<bool>(nullable: false),
                    Attachment = table.Column<string>(maxLength: 500, nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    ContractNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Owner = table.Column<string>(maxLength: 150, nullable: true),
                    Contractor = table.Column<string>(maxLength: 150, nullable: true),
                    ValidDate = table.Column<DateTime>(nullable: false),
                    InvalidDate = table.Column<DateTime>(nullable: false),
                    AlertDays = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 4, nullable: true),
                    Department = table.Column<string>(maxLength: 150, nullable: true),
                    Contents = table.Column<string>(maxLength: 500, nullable: true),
                    Attachment = table.Column<string>(maxLength: 500, nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Valid = table.Column<bool>(nullable: false),
                    Company = table.Column<string>(maxLength: 150, nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    IdentityType = table.Column<string>(maxLength: 10, nullable: false),
                    IdentityNumber = table.Column<string>(maxLength: 30, nullable: false),
                    Ethnicity = table.Column<string>(maxLength: 20, nullable: false),
                    Birth = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(maxLength: 4, nullable: false),
                    Marital = table.Column<string>(maxLength: 4, nullable: true),
                    Education = table.Column<string>(maxLength: 10, nullable: false),
                    GraduateFrom = table.Column<string>(maxLength: 50, nullable: true),
                    GraduationDate = table.Column<DateTime>(nullable: true),
                    Political = table.Column<string>(maxLength: 20, nullable: true),
                    RegisteredResidence = table.Column<string>(maxLength: 200, nullable: false),
                    Residence = table.Column<string>(maxLength: 200, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    MobilePhone = table.Column<string>(maxLength: 20, nullable: false),
                    EmergencyContact = table.Column<string>(maxLength: 20, nullable: true),
                    EmContactPhone = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 20, nullable: true),
                    Certificates = table.Column<string>(maxLength: 200, nullable: true),
                    CertificateDate = table.Column<DateTime>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: true),
                    DimissionDate = table.Column<DateTime>(nullable: true),
                    TermOfContract = table.Column<string>(maxLength: 10, nullable: true),
                    EmployeeStatus = table.Column<string>(maxLength: 4, nullable: true),
                    Valid = table.Column<bool>(nullable: false),
                    EmployeeType = table.Column<string>(maxLength: 4, nullable: true),
                    Company = table.Column<string>(maxLength: 150, nullable: false),
                    Department = table.Column<string>(maxLength: 150, nullable: false),
                    Position = table.Column<string>(nullable: true),
                    LabourCompany = table.Column<string>(maxLength: 150, nullable: false),
                    Attachment = table.Column<string>(maxLength: 500, nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDimission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeName = table.Column<string>(maxLength: 150, nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: true),
                    Company = table.Column<string>(maxLength: 150, nullable: false),
                    Department = table.Column<string>(maxLength: 150, nullable: false),
                    Position = table.Column<string>(maxLength: 150, nullable: true),
                    LabourCompany = table.Column<string>(maxLength: 150, nullable: true),
                    Reason = table.Column<string>(maxLength: 500, nullable: true),
                    DimmissionTime = table.Column<DateTime>(nullable: false),
                    DimmissionType = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDimission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEntry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeName = table.Column<string>(maxLength: 150, nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: true),
                    Company = table.Column<string>(maxLength: 150, nullable: false),
                    Department = table.Column<string>(maxLength: 150, nullable: false),
                    Position = table.Column<string>(maxLength: 150, nullable: true),
                    LabourCompany = table.Column<string>(maxLength: 150, nullable: true),
                    EntryTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Control = table.Column<string>(maxLength: 150, nullable: false),
                    Action = table.Column<string>(maxLength: 150, nullable: false),
                    OrderIndex = table.Column<int>(nullable: true),
                    ParentID = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Valid = table.Column<bool>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    Department = table.Column<string>(maxLength: 150, nullable: false),
                    Type = table.Column<string>(maxLength: 4, nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    ShortName = table.Column<string>(maxLength: 150, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    RegisteredAddress = table.Column<string>(maxLength: 200, nullable: false),
                    Contact = table.Column<string>(maxLength: 150, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    MobilePhone = table.Column<string>(maxLength: 20, nullable: true),
                    Representative = table.Column<string>(maxLength: 150, nullable: false),
                    Establishment = table.Column<DateTime>(nullable: false),
                    OperationPeriod = table.Column<string>(maxLength: 10, nullable: true),
                    RegisteredCapital = table.Column<string>(maxLength: 20, nullable: true),
                    CreditCode = table.Column<string>(maxLength: 30, nullable: false),
                    BusinessType = table.Column<string>(maxLength: 4, nullable: true),
                    IsLabour = table.Column<bool>(nullable: false),
                    Deposit = table.Column<decimal>(nullable: false),
                    Valid = table.Column<bool>(nullable: false),
                    Attachment = table.Column<string>(maxLength: 500, nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferCrossDepart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeName = table.Column<string>(maxLength: 150, nullable: true),
                    DepartmentOut = table.Column<string>(maxLength: 150, nullable: true),
                    PositionOut = table.Column<string>(maxLength: 150, nullable: true),
                    DepartmentIn = table.Column<string>(maxLength: 150, nullable: true),
                    PositionIn = table.Column<string>(maxLength: 150, nullable: true),
                    TransferOutTime = table.Column<DateTime>(nullable: false),
                    OnTheWay = table.Column<string>(maxLength: 10, nullable: true),
                    BackTime = table.Column<DateTime>(nullable: false),
                    TransferedOutBy = table.Column<string>(maxLength: 150, nullable: true),
                    TransferedInBy = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferCrossDepart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferInnerDepart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeName = table.Column<string>(maxLength: 150, nullable: false),
                    PositionOut = table.Column<string>(maxLength: 150, nullable: false),
                    PositionIn = table.Column<string>(maxLength: 150, nullable: false),
                    TransferTime = table.Column<DateTime>(nullable: false),
                    TransferedBy = table.Column<string>(maxLength: 150, nullable: false),
                    TransferType = table.Column<string>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferInnerDepart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    AliasName = table.Column<string>(maxLength: 150, nullable: true),
                    LoginName = table.Column<string>(maxLength: 150, nullable: true),
                    Password = table.Column<string>(maxLength: 150, nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    LastLoginDatetime = table.Column<DateTime>(nullable: true),
                    LoginNumber = table.Column<int>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 150, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 150, nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "EmployeeDimission");

            migrationBuilder.DropTable(
                name: "EmployeeEntry");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "TransferCrossDepart");

            migrationBuilder.DropTable(
                name: "TransferInnerDepart");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
