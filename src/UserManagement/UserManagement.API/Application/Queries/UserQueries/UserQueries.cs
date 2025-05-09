using AutoMapper;
using AutoMapper.QueryableExtensions;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Queries.UserQueries;

public class UserQueries(UserContext context, IMapper mapper)
    : IUserQueries
{
    /// <summary>
    /// Obtiene todos los usuarios en formato básico, optimizando la consulta con AsNoTracking.
    /// </summary>
    public async Task<IEnumerable<BasicUserViewModel>> GetAllUsersAsync()
    {
        return await context.User
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Include(u => u.Identification)
            .ProjectTo<BasicUserViewModel>(mapper.ConfigurationProvider) // Convierte la consulta en SQL optimizado
            .ToListAsync();
    }
    /// <summary>
    /// Obtiene un usuario con todos sus detalles, incluyendo relaciones.
    /// </summary>
    public async Task<FullUserViewModel> GetUserAsync(Guid id)
    {
        return await context.User
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Include(u => u.Sex)
            .Include(u => u.Identification).ThenInclude(i => i.IdentificationType)
            .Include(u => u.CivilStatus)
            .Include(u => u.Language)
            .Include(u => u.Education)
            .Include(u => u.UserAnimals).ThenInclude(ua => ua.Animal)
            .Include(u => u.PreferredProfessional)
            .Include(u => u.PhoneNumbers)
            .Include(u => u.MedicalInformation)
            .Include(u => u.PersonalResources)
            .Include(u => u.WorkCenterResources)
            .ProjectTo<FullUserViewModel>(mapper.ConfigurationProvider) // Convierte la consulta en SQL optimizado
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException(); // Maneja el caso de referencia nula
    }

    public async Task<FullUserViewModel> GetUserByIdentificationNumberAsync(string identificationNumber, Guid workcenterId)
    {
        var userInSameWorkcenter = await context.ServiceContract
            .AsNoTracking()
            .Include(u => u.User)
                .ThenInclude(u => u.Identification)
            .Where(u => u.User.Identification.Number == identificationNumber && u.WorkCenterId == workcenterId)
            .Select(u => u.User)
            .FirstOrDefaultAsync();

        if (userInSameWorkcenter != null)
        {
            throw new InvalidOperationException("El usuario ya existe en el mismo workcenter.");
        }

        return await context.User
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Include(u => u.Sex)
            .Include(u => u.Identification).ThenInclude(i => i.IdentificationType)
            .Include(u => u.CivilStatus)
            .Include(u => u.Language)
            .Include(u => u.Education)
            .Include(u => u.UserAnimals).ThenInclude(ua => ua.Animal)
            .Include(u => u.PreferredProfessional)
            .Include(u => u.PhoneNumbers)
            .ProjectTo<FullUserViewModel>(mapper.ConfigurationProvider) // Convierte la consulta en SQL optimizado
            .FirstOrDefaultAsync(u => u.Identification.Number == identificationNumber) ?? throw new KeyNotFoundException(); // Maneja el caso de referencia nula
    }

    public async Task<MedicalInformationViewModel> GetMedicalInfoByUserIdAsync(Guid userId)
    {
        var medicalInfoId = await context.User
        .Where(u => u.Id == userId)
        .Select(u => u.MedicalInformationId)
        .FirstOrDefaultAsync();

        if (medicalInfoId == null)
        {
            throw new KeyNotFoundException("No se encontró información médica para el usuario especificado.");
        }

        return await context.MedicalInformation
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Include(mi => mi.DependencyDegree)
            .Include(mi => mi.MedicalConditions)
                .ThenInclude(mc => mc.Disease)
            .Include(mi => mi.MedicalConditions)
                .ThenInclude(mc => mc.Status)
            .Include(mi => mi.Medications)
                .ThenInclude(m => m.Medicine)
            .Include(mi => mi.AllergyImpacts)
                .ThenInclude(ai => ai.Allergy)
            .Include(mi => mi.AllergyImpacts)
                .ThenInclude(ai => ai.Severity)
            .Include(mi => mi.HealthCoverages)
            .ProjectTo<MedicalInformationViewModel>(mapper.ConfigurationProvider) // Convierte la consulta en SQL optimizado
            .FirstOrDefaultAsync(u => u.Id == medicalInfoId) ?? throw new KeyNotFoundException(); // Maneja el caso de referencia nula
    }

    /// <summary>
    /// Obtiene las condiciones médicas asociadas a una información médica específica.
    /// </summary>
    /// <param name="medicalInfoId">ID de la información médica.</param>
    /// <returns>Una lista de condiciones médicas asociadas a la información médica.</returns>
    public async Task<IEnumerable<MedicalConditionViewModel>> GetMedicalConditionByMedicalInfoIdAsync(Guid medicalInfoId)
    {
        return await context.MedicalCondition
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Where(mc => mc.MedicalInformationId == medicalInfoId)
            .ProjectTo<MedicalConditionViewModel>(mapper.ConfigurationProvider) // Convierte la consulta en SQL optimizado
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene los impactos de alergia asociados a una información médica específica.
    /// </summary>
    /// <param name="medicalInfoId">ID de la información médica.</param>
    /// <returns>Una lista de impactos de alergia asociados a la información médica.</returns>
    public async Task<IEnumerable<AllergyImpactViewModel>> GetAllergyImpactByMedicalInfoIdAsync(Guid medicalInfoId)
    {
        return await context.AllergyImpact
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Where(ai => ai.MedicalInformationId == medicalInfoId)
            .ProjectTo<AllergyImpactViewModel>(mapper.ConfigurationProvider) // Convierte la consulta en SQL optimizado
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene las coberturas de salud asociadas a una información médica específica.
    /// </summary>
    /// <param name="medicalInfoId">ID de la información médica.</param>
    /// <returns>Una lista de coberturas de salud asociadas a la información médica.</returns>
    public async Task<IEnumerable<HealthCoverageViewModel>> GetHealthCoverageByMedicalInfoIdAsync(Guid medicalInfoId)
    {
        return await context.HealthCoverage
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Where(hc => hc.MedicalInformationId == medicalInfoId)
            .ProjectTo<HealthCoverageViewModel>(mapper.ConfigurationProvider) // Convierte la consulta en SQL optimizado
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene los medicamentos asociados a una información médica específica.
    /// </summary>
    /// <param name="medicalInfoId">ID de la información médica.</param>
    /// <returns>Una lista de medicamentos asociados a la información médica.</returns>
    public async Task<IEnumerable<MedicationViewModel>> GetMedicationByMedicalInfoIdAsync(Guid medicalInfoId)
    {
        return await context.Medication
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Where(m => m.MedicalInformationId == medicalInfoId)
            .ProjectTo<MedicationViewModel>(mapper.ConfigurationProvider) // Convierte la consulta en SQL optimizado
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene todos los registros de una tabla de maestros de forma genérica.
    /// </summary>
    public async Task<IEnumerable<TViewModel>> GetAllMastersAsync<T, TViewModel>()
    where T : class
    where TViewModel : class
    {
        return await context.Set<T>()
            .AsNoTracking()
            .ProjectTo<TViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<WorkCenterResourceViewModel>> GetResourcesByUserIdAsync(Guid userId)
    {
        return await context.WorkCenterResource
            .AsNoTracking()
            .Where(wr => wr.UserId == userId)
            .Include(wr => wr.Resource)
                .ThenInclude(r => r.PhoneNumbers)
            .ProjectTo<WorkCenterResourceViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<PersonalResourceViewModel>> GetPersonalResourcesByUserIdAsync(Guid userId)
    {
        return await context.PersonalResource
            .AsNoTracking()
            .Where(pr => pr.UserId == userId)
            .Include(pr => pr.PhoneNumbers)
            .ProjectTo<PersonalResourceViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserHistoryViewModel>> GetHistoryByUserIdAsync(Guid userId)
    {
        return await context.UserHistory
            .AsNoTracking()
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.OccurredOn)
            .ProjectTo<UserHistoryViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
