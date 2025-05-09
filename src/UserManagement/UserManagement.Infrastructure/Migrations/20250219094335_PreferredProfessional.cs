using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PreferredProfessional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PreferredProfessionalId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PreferredProfessional",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferredProfessional", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PreferredProfessionalId",
                table: "User",
                column: "PreferredProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferredProfessional_ProfessionalId",
                table: "PreferredProfessional",
                column: "ProfessionalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_PreferredProfessional_PreferredProfessionalId",
                table: "User",
                column: "PreferredProfessionalId",
                principalTable: "PreferredProfessional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_PreferredProfessional_PreferredProfessionalId",
                table: "User");

            migrationBuilder.DropTable(
                name: "PreferredProfessional");

            migrationBuilder.DropIndex(
                name: "IX_User_PreferredProfessionalId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PreferredProfessionalId",
                table: "User");
        }
    }
}
