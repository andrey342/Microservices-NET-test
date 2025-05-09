using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Resources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalResource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PhoneNumbers_MobilePhone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    PhoneNumbers_HomePhone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalResource_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumbers_MobilePhone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    PhoneNumbers_HomePhone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    WorkCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkCenterResource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCenterResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkCenterResource_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkCenterResource_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalResource_UserId",
                table: "PersonalResource",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCenterResource_ResourceId",
                table: "WorkCenterResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCenterResource_UserId",
                table: "WorkCenterResource",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalResource");

            migrationBuilder.DropTable(
                name: "WorkCenterResource");

            migrationBuilder.DropTable(
                name: "Resource");
        }
    }
}
