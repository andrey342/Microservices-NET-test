using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Beneficiary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceContractBeneficiary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceContractBeneficiary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceContractBeneficiary_ServiceContract_ServiceContractId",
                        column: x => x.ServiceContractId,
                        principalTable: "ServiceContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceContractBeneficiary_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContractBeneficiary_ServiceContractId_UserId",
                table: "ServiceContractBeneficiary",
                columns: new[] { "ServiceContractId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContractBeneficiary_UserId",
                table: "ServiceContractBeneficiary",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceContractBeneficiary");
        }
    }
}
