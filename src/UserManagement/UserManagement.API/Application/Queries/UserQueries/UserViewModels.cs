 using UserManagement.API.Application.Common.Models;
using UserManagement.API.Application.Queries.WorkCenterQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

namespace UserManagement.API.Application.Queries.UserQueries;

#region User View Models

public record BasicUserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname1 { get; set; } = null!;
    public string Number { get; set; } = null!;

}
public record FullUserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname1 { get; set; } = null!;
    public string? Surname2 { get; set; } = null;
    public string? Appellative { get; set; } = null;
    public string? Email { get; set; } = null;
    public Guid SexId { get; set; }
    public Guid? IdentificationId { get; set; } = null;
    public DateTime Birthdate { get; set; }
    public string BirthdateFormatted => Birthdate.ToString("dd/MM/yyyy");
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - Birthdate.Year;

            // Restar un año si la fecha de cumpleaños aún no ha pasado este año  
            if (Birthdate.Date > today.AddYears(-age))
                age--;

            return age;
        }
    }
    public bool? CongratulateOnBirthDate { get; set; } = null;
    public Guid CivilStatusId { get; set; }
    public Guid? LanguageId { get; set; } = null;
    public Guid? EducationId { get; set; } = null;
    public string? Mobile { get; set; } = null;
    public string? Phone { get; set; } = null;
    public Guid? DependencyId { get; set; } = null;
    public string? CallTime { get; set; } = null;
    public string? Observation { get; set; } = null;
    public Guid? MedicalInformationId { get; set; } = null;
    public Guid? PreferredProfessionalId { get; set; } = null;
    // Relaciones  
    public List<FullUserAnimalViewModel>? UserAnimals { get; set; } = null;
    public DependencyDegreeViewModel? Dependency { get; set; } = null;
    public EducationViewModel? Education { get; set; } = null;
    public LanguageViewModel? Language { get; set; } = null;
    public CivilStatusViewModel CivilStatus { get; set; } = null!;
    public FullIdentificationViewModel Identification { get; set; } = null!;
    public SexViewModel? Sex { get; set; } = null;
    public PreferredProfessionalViewModel? PreferredProfessional { get; set; } = null;
    public MedicalInformationViewModel? MedicalInformation { get; set; } = null;
    public List<PersonalResourceViewModel> PersonalResources { get; set; } = [];
    public List<WorkCenterResourceViewModel> WorkCenterResources { get; set; } = [];

    // Campos de solo lectura  
    public string WorkCenterResourcesNames => WorkCenterResources.Any()
        ? string.Join(", ", WorkCenterResources.Select(w => w.Resource.Name))
        : string.Empty;

    public string PersonalResourcesNames => PersonalResources.Any()
        ? string.Join(", ", PersonalResources.Select(p => p.Name))
        : string.Empty;
}

#endregion

#region Relaciones

public record FullIdentificationViewModel
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public DateTime? ExpirationDate { get; set; } = null;
    public string ExpirationDateFormatted => ExpirationDate.HasValue ? ExpirationDate.Value.ToString("dd/MM/yyyy") : string.Empty;
    public DateTime? UpdateDate { get; set; } = null;
    public string UpdateDateFormatted => UpdateDate.HasValue ? UpdateDate.Value.ToString("dd/MM/yyyy") : string.Empty;

    public Guid TypeId { get; set; }
    public IdentificationTypeViewModel? Type { get; set; }
}

public record PreferredProfessionalViewModel
{
    public Guid Id { get; set; }
    public Guid ProfessionalId { get; set; }
    public string Name { get; set; }
    public string Surname1 { get; set; }
    public string? Surname2 { get; set; } = null;
}

public record FullUserAnimalViewModel
{
    public Guid UserId { get; set; }
    public Guid AnimalId { get; set; }
    public string Name { get; set; } = null!;
    public AnimalViewModel? Animal { get; set; }
}

#region Medical Information View Models
public record MedicalInformationViewModel
{
    public Guid Id { get; set; }
    public Guid? DependencyDegreeId { get; set; }
    public int? BarthelIndex { get; set; }
    public int? LawtonIndex { get; set; }
    public int? PhysicalScaleBSN { get; set; }
    public int? PhysicalScaleBVD { get; set; }
    public int? PsychologicalScaleBSN { get; set; }
    public int? PsychologicalScaleBVD { get; set; }
    public int? SocialScaleBSN { get; set; }
    public int? SocialScaleBVD { get; set; }
    public string? Observation { get; set; }

    public DependencyDegreeViewModel DependencyDegree { get; set; } = null!;
    public List<MedicalConditionViewModel> MedicalConditions { get; set; } = [];
    public List<MedicationViewModel> Medications { get; set; } = [];
    public List<AllergyImpactViewModel> AllergyImpacts { get; set; } = [];
    public List<HealthCoverageViewModel> HealthCoverages { get; set; } = [];
}

public record MedicalConditionViewModel
{
    public Guid Id { get; set; }
    public Guid MedicalInformationId { get; private set; }
    public Guid DiseaseId { get; set; }
    public DiseaseViewModel Disease { get; set; }
    public Guid StatusId { get; set; }
    public MedicalConditionStatusViewModel Status { get; set; }
    public DateTime DiagnosedDate { get; set; }
    public string DiagnosedDateFormatted => DiagnosedDate.ToString("dd/MM/yyyy");
}

public record MedicationViewModel
{
    public Guid Id { get; set; }
    public Guid MedicalInformationId { get; private set; }
    public Guid MedicineId { get; set; }
    public MedicineViewModel Medicine { get; set; }
    public string? Dosage { get; set; }
    public string? Recurrence { get; set; }
}

public record AllergyImpactViewModel
{
    public Guid Id { get; set; }
    public Guid MedicalInformationId { get; private set; }
    public Guid AllergyId { get; set; }
    public AllergyViewModel Allergy { get; set; }
    public Guid SeverityId { get; set; }
    public AllergySeverityViewModel? Severity { get; set; }
    public string? Reaction { get; set; }
}

public record HealthCoverageViewModel
{
    public Guid Id { get; set; }
    public Guid MedicalInformationId { get; private set; }
    public string Provider { get; set; }
    public string PolicyNumber { get; set; }
    public string CoverageType { get; set; }
    public DateTime StartDate { get; set; }
    public string StartDateFormatted => StartDate.ToString("dd/MM/yyyy");
    public DateTime? EndDate { get; set; }
    public string EndDateFormatted => EndDate.HasValue ? EndDate.Value.ToString("dd/MM/yyyy") : string.Empty;
}

#endregion

#region Resources View Models
public record WorkCenterResourceViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ResourceId { get; set; }
    public ResourceBasicViewModel Resource { get; set; }
    public string Observations { get; set; }
}

public record PersonalResourceViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Observations { get; set; }
    public string? Mobile { get; set; } = null;
    public string? Phone { get; set; } = null;
    public Guid UserId { get; set; }
}
#endregion

#region User History View Models
public record UserHistoryViewModel
{
    public Guid Id { get; init; }
    public string Type { get; init; } = null!;
    public string Action { get; init; } = null!;
    public string Description { get; init; } = null!;
    public Guid UserId { get; init; }
    public Guid? ServiceContractId { get; init; }
    public DateTime OccurredOn { get; init; }
    public string OccurredOnFormatted => OccurredOn.ToString("HH:mm dd/MM/yyyy");
}
#endregion

#endregion

#region Maestros

/// <summary>
/// Clases individuales para cada maestro, heredando de MasterViewModel.
/// </summary>
public record IdentificationTypeViewModel : MasterViewModel;

public record AnimalViewModel : MasterViewModel;

public record SexViewModel : MasterViewModel;

public record CivilStatusViewModel : MasterViewModel;

public record LanguageViewModel : MasterViewModel;

public record EducationViewModel : MasterViewModel;

/// <summary>
/// Clase extendida para DependencyDegree con un campo adicional "Description".
/// </summary>
public record DependencyDegreeViewModel : MasterViewModel
{
    public string Description { get; set; } = null!;
}

public record DiseaseViewModel : MasterViewModel;
public record MedicalConditionStatusViewModel : MasterViewModel;
public record MedicineViewModel : MasterViewModel;
public record AllergyViewModel : MasterViewModel;
public record AllergySeverityViewModel : MasterViewModel
{
    public int Order { get; set; }
}

#endregion