using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Cohabitant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CohabitantType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CohabitantType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cohabitant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResidenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumbers_MobilePhone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    PhoneNumbers_HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CohabitantTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cohabitant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cohabitant_CohabitantType_CohabitantTypeId",
                        column: x => x.CohabitantTypeId,
                        principalTable: "CohabitantType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cohabitant_Residence_ResidenceId",
                        column: x => x.ResidenceId,
                        principalTable: "Residence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cohabitant_CohabitantTypeId",
                table: "Cohabitant",
                column: "CohabitantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cohabitant_ResidenceId",
                table: "Cohabitant",
                column: "ResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_CohabitantType_Name",
                table: "CohabitantType",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cohabitant");

            migrationBuilder.DropTable(
                name: "CohabitantType");
        }
    }
}
