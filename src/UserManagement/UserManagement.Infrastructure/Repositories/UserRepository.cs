namespace UserManagement.Infrastructure.Repositories;
public class UserRepository: IUserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Identification?> GetIdentificationByNumberAsync(string number)
    {
        return await _context.Identification
            .FirstOrDefaultAsync(i => i.Number == number);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.User
            .Include(u => u.Sex)
            .Include(u => u.Identification)
                .ThenInclude(u => u.IdentificationType)
            .Include(u => u.CivilStatus)
            .Include(u => u.Language)
            .Include(u => u.Education)
            .Include(u => u.UserAnimals)
                .ThenInclude(ua => ua.Animal)
            .Include(u => u.MedicalInformation)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<PreferredProfessional?> GetPreferredProfessionalByProfessionalId(Guid professionalId)
    {
        return await _context.PreferredProfessional
            .FirstOrDefaultAsync(pp => pp.ProfessionalId == professionalId);
    }

    public async Task<MedicalInformation?> GetMedicalInformationById(Guid medicalInformationId)
    {
        return await _context.MedicalInformation
            .FirstOrDefaultAsync(mi => mi.Id == medicalInformationId);
    }

    public async Task<AllergyImpact?> GetAllergyImpactById(Guid allergyImpactId)
    {
        return await _context.AllergyImpact
            .FirstOrDefaultAsync(mi => mi.Id == allergyImpactId);
    }

    public async Task<Medication?> GetMedicationById(Guid medicationId)
    {
        return await _context.Medication
            .FirstOrDefaultAsync(mi => mi.Id == medicationId);
    }

    public async Task<HealthCoverage?> GetHealthCoverageById(Guid healthCoverageId)
    {
        return await _context.HealthCoverage
            .FirstOrDefaultAsync(mi => mi.Id == healthCoverageId);
    }

    public async Task<MedicalCondition?> GetMedicalConditionById(Guid medicalConditionId)
    {
        return await _context.MedicalCondition
            .Include(mc => mc.MedicalInformation)
            .FirstOrDefaultAsync(mi => mi.Id == medicalConditionId);
    }

    public void Add(User user)
    {
        _context.User.Add(user);
    }

    public void Update(User user)
    {
        _context.Attach(user).State = EntityState.Modified;
    }

    public void Delete(User user)
    {
        _context.User.Remove(user);
    }

    public void AddUserHistory(UserHistory userHistory)
    {
        _context.UserHistory.Add(userHistory);
    }

    public void AddWorkCenterResource(WorkCenterResource entity)
    {
        _context.WorkCenterResource.Add(entity);
    }

    public void UpdateWorkCenterResource(WorkCenterResource entity)
    {
        _context.WorkCenterResource.Update(entity);
    }

    public void DeleteWorkCenterResource(WorkCenterResource entity)
    {
        _context.WorkCenterResource.Remove(entity);
    }

    public async Task<WorkCenterResource?> GetWorkCenterResourceByIdAsync(Guid id)
    {
        return await _context.WorkCenterResource
            .FirstOrDefaultAsync(wr => wr.Id == id);
    }

    public void AddPersonalResource(PersonalResource entity)
    {
        _context.PersonalResource.Add(entity);
    }

    public void UpdatePersonalResource(PersonalResource entity)
    {
        _context.Attach(entity).State = EntityState.Modified;
    }

    public void DeletePersonalResource(PersonalResource entity)
    {
        _context.PersonalResource.Remove(entity);
    }

    public async Task<PersonalResource?> GetPersonalResourceByIdAsync(Guid id)
    {
        return await _context.PersonalResource.FirstOrDefaultAsync(x => x.Id == id);
    }

}
