using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MedicalInformationNewInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_DependencyDegree_DependencyId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_MedicalInformation_MedicalInformationId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_DependencyId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DependencyId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhysicalScale",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "PsychologicalScale",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "SensoryCharacteristics",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "SocialScale",
                table: "MedicalInformation");

            migrationBuilder.AddColumn<int>(
                name: "BarthelIndex",
                table: "MedicalInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DependencyDegreeId",
                table: "MedicalInformation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LawtonIndex",
                table: "MedicalInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhysicalScaleBSN",
                table: "MedicalInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhysicalScaleBVD",
                table: "MedicalInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PsychologicalScaleBSN",
                table: "MedicalInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PsychologicalScaleBVD",
                table: "MedicalInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SocialScaleBSN",
                table: "MedicalInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SocialScaleBVD",
                table: "MedicalInformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInformation_DependencyDegreeId",
                table: "MedicalInformation",
                column: "DependencyDegreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalInformation_DependencyDegree_DependencyDegreeId",
                table: "MedicalInformation",
                column: "DependencyDegreeId",
                principalTable: "DependencyDegree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_MedicalInformation_MedicalInformationId",
                table: "User",
                column: "MedicalInformationId",
                principalTable: "MedicalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalInformation_DependencyDegree_DependencyDegreeId",
                table: "MedicalInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_User_MedicalInformation_MedicalInformationId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_MedicalInformation_DependencyDegreeId",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "BarthelIndex",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "DependencyDegreeId",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "LawtonIndex",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "PhysicalScaleBSN",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "PhysicalScaleBVD",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "PsychologicalScaleBSN",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "PsychologicalScaleBVD",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "SocialScaleBSN",
                table: "MedicalInformation");

            migrationBuilder.DropColumn(
                name: "SocialScaleBVD",
                table: "MedicalInformation");

            migrationBuilder.AddColumn<Guid>(
                name: "DependencyId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalScale",
                table: "MedicalInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PsychologicalScale",
                table: "MedicalInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SensoryCharacteristics",
                table: "MedicalInformation",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialScale",
                table: "MedicalInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_User_DependencyId",
                table: "User",
                column: "DependencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_DependencyDegree_DependencyId",
                table: "User",
                column: "DependencyId",
                principalTable: "DependencyDegree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_MedicalInformation_MedicalInformationId",
                table: "User",
                column: "MedicalInformationId",
                principalTable: "MedicalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
