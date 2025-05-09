using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ServiceContractUserTypeUserTypology : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserTypeId",
                table: "ServiceContract",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserTypologyId",
                table: "ServiceContract",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContract_UserTypeId",
                table: "ServiceContract",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContract_UserTypologyId",
                table: "ServiceContract",
                column: "UserTypologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceContract_UserType_UserTypeId",
                table: "ServiceContract",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceContract_UserTypology_UserTypologyId",
                table: "ServiceContract",
                column: "UserTypologyId",
                principalTable: "UserTypology",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceContract_UserType_UserTypeId",
                table: "ServiceContract");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceContract_UserTypology_UserTypologyId",
                table: "ServiceContract");

            migrationBuilder.DropIndex(
                name: "IX_ServiceContract_UserTypeId",
                table: "ServiceContract");

            migrationBuilder.DropIndex(
                name: "IX_ServiceContract_UserTypologyId",
                table: "ServiceContract");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "ServiceContract");

            migrationBuilder.DropColumn(
                name: "UserTypologyId",
                table: "ServiceContract");
        }
    }
}
