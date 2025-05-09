using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.API.Application.Commands.MedicalInformationCommands.CreateMedicalInformation;
using UserManagement.API.Application.Commands.MedicalConditionCommands.AddMedicalCondition;
using UserManagement.API.Application.Commands.MedicalConditionCommands.UpdateMedicalCondition;
using UserManagement.API.Application.Commands.AllergyImpactCommands.CreateAllergyImpact;
using UserManagement.API.Application.Commands.AllergyImpactCommands.UpdateAllergyImpact;
using UserManagement.API.Application.Commands.HealthCoverageCommands.UpdateHealthCoverage;
using UserManagement.API.Application.Commands.HealthCoverageCommands.CreateHealthCoverage;
using UserManagement.API.Application.Commands.MedicationCommands.CreateMedication;
using UserManagement.API.Application.Commands.MedicationCommands.UpdateMedication;

namespace UserManagement.API.Application.Common.Mapping.MedicalInformationMappings;

public class MedicalInformationMappingProfile : Profile
{
    public MedicalInformationMappingProfile()
    {
        #region View Models Mapping (Lectura)
        CreateMap<MedicalInformation, MedicalInformationViewModel>();

        CreateMap<MedicalCondition, MedicalConditionViewModel>();
        CreateMap<Medication, MedicationViewModel>();
        CreateMap<AllergyImpact, AllergyImpactViewModel>();
        CreateMap<HealthCoverage, HealthCoverageViewModel>();

        // Maestros
        CreateMap<Disease, DiseaseViewModel>();
        CreateMap<MedicalConditionStatus, MedicalConditionStatusViewModel>();
        CreateMap<Medicine, MedicineViewModel>();
        CreateMap<Allergy, AllergyViewModel>();
        CreateMap<AllergySeverity, AllergySeverityViewModel>();
        #endregion

        #region Create Mapping (Creación)
        CreateMap<CreateMedicalInformationRequest, MedicalInformation>();
        CreateMap<CreateMedicalConditionRequest, MedicalCondition>();
        CreateMap<CreateAllergyImpactRequest, AllergyImpact>();
        CreateMap<CreateHealthCoverageRequest, HealthCoverage>();
        CreateMap<CreateMedicationRequest, Medication>();
        #endregion

        #region Update Mapping (Actualización)
        CreateMap<UpdateMedicalConditionRequest, MedicalCondition>();
        CreateMap<UpdateAllergyImpactRequest, AllergyImpact>();
        CreateMap<UpdateHealthCoverageRequest, HealthCoverage>();
        CreateMap<UpdateMedicationRequest, Medication>();
        #endregion
    }
}