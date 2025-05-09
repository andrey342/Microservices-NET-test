using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientRequestAndEventLock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_requests",
                table: "requests");

            migrationBuilder.RenameTable(
                name: "requests",
                newName: "ClientRequest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientRequest",
                table: "ClientRequest",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EventLock",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LockedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLock", x => x.EventId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventLock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientRequest",
                table: "ClientRequest");

            migrationBuilder.RenameTable(
                name: "ClientRequest",
                newName: "requests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_requests",
                table: "requests",
                column: "Id");
        }
    }
}
