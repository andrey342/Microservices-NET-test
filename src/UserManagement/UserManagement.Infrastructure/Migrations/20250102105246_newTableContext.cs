using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newTableContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Identification_IdentificationId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Sex_SexId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Identification");

            migrationBuilder.AddColumn<string>(
                name: "Appellative",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CallTime",
                table: "User",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<Guid>(
                name: "CivilStatusId",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "CongratulateOnBirthDate",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DependencyId",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EducationId",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "User",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "User",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "User",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname2",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sex",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Identification",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Identification",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CivilStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CivilStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DependencyDegree",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependencyDegree", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentificationType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAnimal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnimal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnimal_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnimal_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_CivilStatusId",
                table: "User",
                column: "CivilStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_User_DependencyId",
                table: "User",
                column: "DependencyId");

            migrationBuilder.CreateIndex(
                name: "IX_User_EducationId",
                table: "User",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LanguageId",
                table: "User",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Identification_TypeId",
                table: "Identification",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimal_AnimalId",
                table: "UserAnimal",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimal_UserId",
                table: "UserAnimal",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Identification_IdentificationType_TypeId",
                table: "Identification",
                column: "TypeId",
                principalTable: "IdentificationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_CivilStatus_CivilStatusId",
                table: "User",
                column: "CivilStatusId",
                principalTable: "CivilStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_DependencyDegree_DependencyId",
                table: "User",
                column: "DependencyId",
                principalTable: "DependencyDegree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Education_EducationId",
                table: "User",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Identification_IdentificationId",
                table: "User",
                column: "IdentificationId",
                principalTable: "Identification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Language_LanguageId",
                table: "User",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Sex_SexId",
                table: "User",
                column: "SexId",
                principalTable: "Sex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Identification_IdentificationType_TypeId",
                table: "Identification");

            migrationBuilder.DropForeignKey(
                name: "FK_User_CivilStatus_CivilStatusId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_DependencyDegree_DependencyId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Education_EducationId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Identification_IdentificationId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Language_LanguageId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Sex_SexId",
                table: "User");

            migrationBuilder.DropTable(
                name: "CivilStatus");

            migrationBuilder.DropTable(
                name: "DependencyDegree");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "IdentificationType");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "UserAnimal");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_User_CivilStatusId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_DependencyId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_EducationId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_LanguageId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Identification_TypeId",
                table: "Identification");

            migrationBuilder.DropColumn(
                name: "Appellative",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CallTime",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CivilStatusId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CongratulateOnBirthDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DependencyId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Surname2",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Identification");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sex",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Identification",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Identification",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Identification_IdentificationId",
                table: "User",
                column: "IdentificationId",
                principalTable: "Identification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Sex_SexId",
                table: "User",
                column: "SexId",
                principalTable: "Sex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
