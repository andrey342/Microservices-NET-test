using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KeyStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyHistory_KeyStatus_KeyStatusId",
                table: "KeyHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyHistory_Key_KeyId",
                table: "KeyHistory");

            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "KeyStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentStatusId",
                table: "Key",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_KeyStatus_Default",
                table: "KeyStatus",
                column: "Default",
                unique: true,
                filter: "[Default] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Key_CurrentStatusId",
                table: "Key",
                column: "CurrentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Key_KeyStatus_CurrentStatusId",
                table: "Key",
                column: "CurrentStatusId",
                principalTable: "KeyStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyHistory_KeyStatus_KeyStatusId",
                table: "KeyHistory",
                column: "KeyStatusId",
                principalTable: "KeyStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyHistory_Key_KeyId",
                table: "KeyHistory",
                column: "KeyId",
                principalTable: "Key",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Key_KeyStatus_CurrentStatusId",
                table: "Key");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyHistory_KeyStatus_KeyStatusId",
                table: "KeyHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyHistory_Key_KeyId",
                table: "KeyHistory");

            migrationBuilder.DropIndex(
                name: "IX_KeyStatus_Default",
                table: "KeyStatus");

            migrationBuilder.DropIndex(
                name: "IX_Key_CurrentStatusId",
                table: "Key");

            migrationBuilder.DropColumn(
                name: "Default",
                table: "KeyStatus");

            migrationBuilder.DropColumn(
                name: "CurrentStatusId",
                table: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyHistory_KeyStatus_KeyStatusId",
                table: "KeyHistory",
                column: "KeyStatusId",
                principalTable: "KeyStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyHistory_Key_KeyId",
                table: "KeyHistory",
                column: "KeyId",
                principalTable: "Key",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
