using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ServiceContractIndexUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceContract_UserId_WorkCenterId",
                table: "ServiceContract");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContract_UserId",
                table: "ServiceContract",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceContract_UserId",
                table: "ServiceContract");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContract_UserId_WorkCenterId",
                table: "ServiceContract",
                columns: new[] { "UserId", "WorkCenterId" },
                unique: true);
        }
    }
}
