using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AreaParent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Area",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Area_AreaLevelId",
                table: "Area",
                column: "AreaLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_ParentId",
                table: "Area",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Area_AreaLevel_AreaLevelId",
                table: "Area",
                column: "AreaLevelId",
                principalTable: "AreaLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Area_Area_ParentId",
                table: "Area",
                column: "ParentId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Area_AreaLevel_AreaLevelId",
                table: "Area");

            migrationBuilder.DropForeignKey(
                name: "FK_Area_Area_ParentId",
                table: "Area");

            migrationBuilder.DropIndex(
                name: "IX_Area_AreaLevelId",
                table: "Area");

            migrationBuilder.DropIndex(
                name: "IX_Area_ParentId",
                table: "Area");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Area");
        }
    }
}
