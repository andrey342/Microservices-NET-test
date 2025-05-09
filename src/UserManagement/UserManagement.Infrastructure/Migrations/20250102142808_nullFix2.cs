using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nullFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_DependencyDegree_DependencyId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Education_EducationId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Language_LanguageId",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_DependencyDegree_DependencyId",
                table: "User",
                column: "DependencyId",
                principalTable: "DependencyDegree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Education_EducationId",
                table: "User",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Language_LanguageId",
                table: "User",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_DependencyDegree_DependencyId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Education_EducationId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Language_LanguageId",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_DependencyDegree_DependencyId",
                table: "User",
                column: "DependencyId",
                principalTable: "DependencyDegree",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Education_EducationId",
                table: "User",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Language_LanguageId",
                table: "User",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id");
        }
    }
}
