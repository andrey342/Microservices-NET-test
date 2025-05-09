using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteKeyHistoryCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyHistory_Key_KeyId",
                table: "KeyHistory");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyHistory_Key_KeyId",
                table: "KeyHistory",
                column: "KeyId",
                principalTable: "Key",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyHistory_Key_KeyId",
                table: "KeyHistory");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyHistory_Key_KeyId",
                table: "KeyHistory",
                column: "KeyId",
                principalTable: "Key",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
