using UserManagement.API.Application.Queries.WorkCenterQueries;

namespace UserManagement.API.Application.Queries.UserQueries;

public interface IUserQueries
{
    /// <summary>
    /// Obtiene una lista de todos los usuarios en formato básico.
    /// </summary>
    Task<IEnumerable<BasicUserViewModel>> GetAllUsersAsync();

    /// <summary>
    /// Obtiene los detalles completos de un usuario por su ID.
    /// </summary>
    Task<FullUserViewModel> GetUserAsync(Guid id);
    /// <summary>
    /// Obtiene los detalles completos de un usuario por su ID.
    /// </summary>
    Task<FullUserViewModel> GetUserByIdentificationNumberAsync(string identificationNumber, Guid workcenterId);
    
    /// <summary>
    /// Obtiene la información médica de un usuario por su ID.
    /// </summary>
    Task<MedicalInformationViewModel> GetMedicalInfoByUserIdAsync(Guid id);

    /// <summary>
    /// Obtiene las condiciones médicas asociadas a una información médica específica.
    /// </summary>
    Task<IEnumerable<MedicalConditionViewModel>> GetMedicalConditionByMedicalInfoIdAsync(Guid medicalInfoId);

    /// <summary>
    /// Obtiene las alergias asociadas a una información médica específica.
    /// </summary>
    /// <param name="medicalInfoId">ID de la información médica.</param>
    /// <returns>Una lista de alergias asociadas a la información médica.</returns>
    Task<IEnumerable<AllergyImpactViewModel>> GetAllergyImpactByMedicalInfoIdAsync(Guid medicalInfoId);
    
    /// <summary>
    /// Obtiene las coberturas de salud asociadas a una información médica específica.
    /// </summary>
    /// <param name="medicalInfoId">ID de la información médica.</param>
    /// <returns>Una lista de coberturas de salud asociadas a la información médica.</returns>
    Task<IEnumerable<HealthCoverageViewModel>> GetHealthCoverageByMedicalInfoIdAsync(Guid medicalInfoId);

    /// <summary>
    /// Obtiene los medicamentos asociados a una información médica específica.
    /// </summary>
    /// <param name="medicalInfoId">ID de la información médica.</param>
    /// <returns>Una lista de medicamentos asociados a la información médica.</returns>
    Task<IEnumerable<MedicationViewModel>> GetMedicationByMedicalInfoIdAsync(Guid medicalInfoId);

    /// <summary>
    /// Obtiene de forma genérica todos los registros de una tabla maestra y los proyecta a su ViewModel.
    /// </summary>
    Task<IEnumerable<TViewModel>> GetAllMastersAsync<T, TViewModel>()
        where T : class
        where TViewModel : class;

    Task<IEnumerable<WorkCenterResourceViewModel>> GetResourcesByUserIdAsync(Guid userId);
    Task<IEnumerable<PersonalResourceViewModel>> GetPersonalResourcesByUserIdAsync(Guid userId);
    Task<IEnumerable<UserHistoryViewModel>> GetHistoryByUserIdAsync(Guid userId);
}
