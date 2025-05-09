using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Contract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "ContractStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_ContractStatus_CurrentStatusId",
                        column: x => x.CurrentStatusId,
                        principalTable: "ContractStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_WorkCenter_WorkCenterId",
                        column: x => x.WorkCenterId,
                        principalTable: "WorkCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Residence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address_RoadType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_StreetName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address_Town = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Door = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address_Floor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address_Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address_Stair = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address_Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Address_Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Elevator = table.Column<bool>(type: "bit", nullable: false),
                    Concierge = table.Column<bool>(type: "bit", nullable: false),
                    Doorman = table.Column<bool>(type: "bit", nullable: false),
                    FireHydrant = table.Column<bool>(type: "bit", nullable: false),
                    Wifi = table.Column<bool>(type: "bit", nullable: false),
                    Gas = table.Column<bool>(type: "bit", nullable: false),
                    Electricity = table.Column<bool>(type: "bit", nullable: false),
                    Water = table.Column<bool>(type: "bit", nullable: false),
                    Internet = table.Column<bool>(type: "bit", nullable: false),
                    ArchitecturalBarrierEntrance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ArchitecturalBarriereResidence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Observation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsCurrentResidence = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Residence_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Contract_CurrentStatusId",
                table: "Contract",
                column: "CurrentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ServiceTypeId",
                table: "Contract",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_UserId_WorkCenterId",
                table: "Contract",
                columns: new[] { "UserId", "WorkCenterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_WorkCenterId",
                table: "Contract",
                column: "WorkCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Residence_ContractId_IsCurrentResidence",
                table: "Residence",
                columns: new[] { "ContractId", "IsCurrentResidence" },
                unique: true,
                filter: "[IsCurrentResidence] = 1");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "KeyStatus");

            migrationBuilder.DropTable(
                name: "Residence");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "ContractStatus");

            migrationBuilder.DropTable(
                name: "ServiceType");

        }
    }
}
