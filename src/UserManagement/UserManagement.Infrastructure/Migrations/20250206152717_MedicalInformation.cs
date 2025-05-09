using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MedicalInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "User",
                newName: "Surname1");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicalInformationId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Allergy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllergySeverity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergySeverity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disease",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disease", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalConditionStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalConditionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PsychologicalScale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhysicalScale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SocialScale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SensoryCharacteristics = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Observation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllergyImpact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllergyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeverityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reaction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergyImpact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllergyImpact_AllergySeverity_SeverityId",
                        column: x => x.SeverityId,
                        principalTable: "AllergySeverity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllergyImpact_Allergy_AllergyId",
                        column: x => x.AllergyId,
                        principalTable: "Allergy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllergyImpact_MedicalInformation_MedicalInformationId",
                        column: x => x.MedicalInformationId,
                        principalTable: "MedicalInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthCoverage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PolicyNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CoverageType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCoverage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthCoverage_MedicalInformation_MedicalInformationId",
                        column: x => x.MedicalInformationId,
                        principalTable: "MedicalInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalCondition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiseaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnosedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCondition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalCondition_Disease_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Disease",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalCondition_MedicalConditionStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "MedicalConditionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalCondition_MedicalInformation_MedicalInformationId",
                        column: x => x.MedicalInformationId,
                        principalTable: "MedicalInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Recurrence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medication_MedicalInformation_MedicalInformationId",
                        column: x => x.MedicalInformationId,
                        principalTable: "MedicalInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medication_Medicine_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_MedicalInformationId",
                table: "User",
                column: "MedicalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergyImpact_AllergyId",
                table: "AllergyImpact",
                column: "AllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergyImpact_MedicalInformationId",
                table: "AllergyImpact",
                column: "MedicalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergyImpact_SeverityId",
                table: "AllergyImpact",
                column: "SeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthCoverage_MedicalInformationId",
                table: "HealthCoverage",
                column: "MedicalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCondition_DiseaseId",
                table: "MedicalCondition",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCondition_MedicalInformationId",
                table: "MedicalCondition",
                column: "MedicalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCondition_StatusId",
                table: "MedicalCondition",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_MedicalInformationId",
                table: "Medication",
                column: "MedicalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_MedicineId",
                table: "Medication",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_MedicalInformation_MedicalInformationId",
                table: "User",
                column: "MedicalInformationId",
                principalTable: "MedicalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_MedicalInformation_MedicalInformationId",
                table: "User");

            migrationBuilder.DropTable(
                name: "AllergyImpact");

            migrationBuilder.DropTable(
                name: "HealthCoverage");

            migrationBuilder.DropTable(
                name: "MedicalCondition");

            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.DropTable(
                name: "AllergySeverity");

            migrationBuilder.DropTable(
                name: "Allergy");

            migrationBuilder.DropTable(
                name: "Disease");

            migrationBuilder.DropTable(
                name: "MedicalConditionStatus");

            migrationBuilder.DropTable(
                name: "MedicalInformation");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropIndex(
                name: "IX_User_MedicalInformationId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MedicalInformationId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Surname1",
                table: "User",
                newName: "Surname");
        }
    }
}
