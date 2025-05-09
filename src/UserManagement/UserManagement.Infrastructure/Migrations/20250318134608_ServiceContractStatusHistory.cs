using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ServiceContractStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceContractStatusReason",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ServiceContractStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceContractStatusReason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceContractStatusReason_ServiceContractStatus_ServiceContractStatusId",
                        column: x => x.ServiceContractStatusId,
                        principalTable: "ServiceContractStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceContractStatusHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceContractStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceContractStatusReasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceContractStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceContractStatusHistory_ServiceContractStatusReason_ServiceContractStatusReasonId",
                        column: x => x.ServiceContractStatusReasonId,
                        principalTable: "ServiceContractStatusReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceContractStatusHistory_ServiceContractStatus_ServiceContractStatusId",
                        column: x => x.ServiceContractStatusId,
                        principalTable: "ServiceContractStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceContractStatusHistory_ServiceContract_ServiceContractId",
                        column: x => x.ServiceContractId,
                        principalTable: "ServiceContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContractStatusHistory_ServiceContractId",
                table: "ServiceContractStatusHistory",
                column: "ServiceContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContractStatusHistory_ServiceContractStatusId",
                table: "ServiceContractStatusHistory",
                column: "ServiceContractStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContractStatusHistory_ServiceContractStatusReasonId",
                table: "ServiceContractStatusHistory",
                column: "ServiceContractStatusReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContractStatusReason_ServiceContractStatusId",
                table: "ServiceContractStatusReason",
                column: "ServiceContractStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceContractStatusHistory");

            migrationBuilder.DropTable(
                name: "ServiceContractStatusReason");
        }
    }
}
