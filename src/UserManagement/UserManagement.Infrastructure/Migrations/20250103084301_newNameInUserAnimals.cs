using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newNameInUserAnimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimal_Animal_AnimalId",
                table: "UserAnimal");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserAnimal",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimal_Animal_AnimalId",
                table: "UserAnimal",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimal_Animal_AnimalId",
                table: "UserAnimal");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserAnimal");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimal_Animal_AnimalId",
                table: "UserAnimal",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
