using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ServiceContractDevices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentralUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceContractCentralUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CentralUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceContractCentralUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceContractCentralUnits_CentralUnits_CentralUnitId",
                        column: x => x.CentralUnitId,
                        principalTable: "CentralUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceContractCentralUnits_ServiceContract_ServiceContractId",
                        column: x => x.ServiceContractId,
                        principalTable: "ServiceContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Peripheral",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceContractCentralUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peripheral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peripheral_ServiceContractCentralUnits_ServiceContractCentralUnitId",
                        column: x => x.ServiceContractCentralUnitId,
                        principalTable: "ServiceContractCentralUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peripheral_ServiceContractCentralUnitId",
                table: "Peripheral",
                column: "ServiceContractCentralUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContractCentralUnits_CentralUnitId_ServiceContractId",
                table: "ServiceContractCentralUnits",
                columns: new[] { "CentralUnitId", "ServiceContractId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContractCentralUnits_ServiceContractId",
                table: "ServiceContractCentralUnits",
                column: "ServiceContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peripheral");

            migrationBuilder.DropTable(
                name: "ServiceContractCentralUnits");

            migrationBuilder.DropTable(
                name: "CentralUnits");
        }
    }
}
