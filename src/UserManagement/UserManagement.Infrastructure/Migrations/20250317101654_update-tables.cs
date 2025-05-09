using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residence_ServiceContract_ServiceContractId",
                table: "Residence");

            migrationBuilder.AddForeignKey(
                name: "FK_Residence_ServiceContract_ServiceContractId",
                table: "Residence",
                column: "ServiceContractId",
                principalTable: "ServiceContract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residence_ServiceContract_ServiceContractId",
                table: "Residence");

            migrationBuilder.AddForeignKey(
                name: "FK_Residence_ServiceContract_ServiceContractId",
                table: "Residence",
                column: "ServiceContractId",
                principalTable: "ServiceContract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
