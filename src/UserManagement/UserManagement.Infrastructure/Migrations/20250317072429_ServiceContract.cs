using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ServiceContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residence_Contract_ContractId",
                table: "Residence");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "ContractStatus");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Residence",
                newName: "ServiceContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Residence_ContractId_IsCurrentResidence",
                table: "Residence",
                newName: "IX_Residence_ServiceContractId_IsCurrentResidence");

            migrationBuilder.CreateTable(
                name: "ServiceContractStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceContractStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceContract",
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
                    table.PrimaryKey("PK_ServiceContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceContract_ServiceContractStatus_CurrentStatusId",
                        column: x => x.CurrentStatusId,
                        principalTable: "ServiceContractStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceContract_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceContract_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceContract_WorkCenter_WorkCenterId",
                        column: x => x.WorkCenterId,
                        principalTable: "WorkCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContract_CurrentStatusId",
                table: "ServiceContract",
                column: "CurrentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContract_ServiceTypeId",
                table: "ServiceContract",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContract_UserId_WorkCenterId",
                table: "ServiceContract",
                columns: new[] { "UserId", "WorkCenterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContract_WorkCenterId",
                table: "ServiceContract",
                column: "WorkCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContractStatus_Default",
                table: "ServiceContractStatus",
                column: "Default",
                unique: true,
                filter: "[Default] = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Residence_ServiceContract_ServiceContractId",
                table: "Residence",
                column: "ServiceContractId",
                principalTable: "ServiceContract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residence_ServiceContract_ServiceContractId",
                table: "Residence");

            migrationBuilder.DropTable(
                name: "ServiceContract");

            migrationBuilder.DropTable(
                name: "ServiceContractStatus");

            migrationBuilder.RenameColumn(
                name: "ServiceContractId",
                table: "Residence",
                newName: "ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Residence_ServiceContractId_IsCurrentResidence",
                table: "Residence",
                newName: "IX_Residence_ContractId_IsCurrentResidence");

            migrationBuilder.CreateTable(
                name: "ContractStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "IX_ContractStatus_Default",
                table: "ContractStatus",
                column: "Default",
                unique: true,
                filter: "[Default] = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Residence_Contract_ContractId",
                table: "Residence",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
