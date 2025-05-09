using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Repositories;
public interface IUserRepository: IRepository<User>
{
    Task<Identification?> GetIdentificationByNumberAsync(string number);
    Task<User?> GetByIdAsync(Guid id);
    Task<PreferredProfessional?> GetPreferredProfessionalByProfessionalId(Guid professionalId);
    Task<MedicalInformation?> GetMedicalInformationById(Guid medicalInformationId);
    Task<AllergyImpact?> GetAllergyImpactById(Guid allergyImpactId);
    Task<Medication?> GetMedicationById(Guid medicationId);
    Task<HealthCoverage?> GetHealthCoverageById(Guid healthCoverageId);
    Task<MedicalCondition?> GetMedicalConditionById(Guid medicalConditionId);
    void Add(User user);
    void Update(User user);
    void Delete(User user);
    void AddUserHistory(UserHistory userHistory); // Añadir método para manejar UserHistory
    void AddWorkCenterResource(WorkCenterResource entity);
    void UpdateWorkCenterResource(WorkCenterResource entity);
    void DeleteWorkCenterResource(WorkCenterResource entity);
    Task<WorkCenterResource?> GetWorkCenterResourceByIdAsync(Guid id);
    void AddPersonalResource(PersonalResource entity);
    void UpdatePersonalResource(PersonalResource entity);
    void DeletePersonalResource(PersonalResource entity);
    Task<PersonalResource?> GetPersonalResourceByIdAsync(Guid id);
}