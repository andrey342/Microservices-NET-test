using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.Events;
using UserManagement.Domain.SeedWork;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.AggregatesModel.UserAggregate;
public class User : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Surname1 { get; private set; }
    public string? Surname2 { get; private set; } = null;
    public string? Appellative { get; private set; } = null;
    public Email? Email { get; private set; } = null;
    public Guid SexId { get; private set; }
    public Sex? Sex { get; private set; } = null;
    public Guid? IdentificationId { get; private set; } = null;
    public Identification? Identification { get; private set; } = null;
    public DateTime Birthdate { get; private set; }
    public bool? CongratulateOnBirthDate { get; private set; } = null;
    public Guid CivilStatusId { get; private set; }
    public CivilStatus CivilStatus { get; private set; }
    public Guid? LanguageId { get; private set; } = null;
    public Language? Language { get; private set; } = null;
    public Guid? EducationId { get; private set; } = null;
    public Education? Education { get; private set; } = null;
    public PhoneNumbers? PhoneNumbers { get; private set; } = null;
    public CallTime? CallTime { get; private set; } = null;
    public string? Observation { get; private set; } = null;
    public Guid? MedicalInformationId { get; private set; }
    public MedicalInformation? MedicalInformation { get; private set; }

    private List<UserAnimal> _userAnimals = new List<UserAnimal>();
    public IReadOnlyCollection<UserAnimal> UserAnimals => _userAnimals.AsReadOnly();

    public Guid? PreferredProfessionalId { get; private set; }
    public PreferredProfessional? PreferredProfessional { get; private set; }

    private List<UserHistory> _userHistories = new List<UserHistory>();
    public IReadOnlyCollection<UserHistory> UserHistories => _userHistories.AsReadOnly();
    private List<ServiceContractBeneficiary> _serviceContractsBeneficiary = new List<ServiceContractBeneficiary>();
    public IReadOnlyCollection<ServiceContractBeneficiary> ServiceContractsBeneficiary => _serviceContractsBeneficiary.AsReadOnly();
    
    private List<PersonalResource> _personalResources = new List<PersonalResource>();
    public IReadOnlyCollection<PersonalResource> PersonalResources => _personalResources.AsReadOnly();
    
    private List<WorkCenterResource> _workCenterResources = new List<WorkCenterResource>();
    public IReadOnlyCollection<WorkCenterResource> WorkCenterResources => _workCenterResources.AsReadOnly();
    private User() { }

    public User(User user)
    {
        // EVITA Copiar propiedad por propiedad (CopyPropertiesFast para +10 propiedades)
        this.CopyPropertiesFast(user);
        // Crear evento solo con los datos que necesitan otros consumidores (Microservicios)
        AddDomainEvent(new UserCreatedDomainEvent(Id, Name, Email?.Value));
    }

    public void Update(User user)
    {
        // EVITA Copiar propiedad por propiedad (CopyPropertiesFast para +10 propiedades)
        this.CopyPropertiesFast(user);
        // Crear evento solo con los datos que necesitan otros consumidores (Microservicios)
        AddDomainEvent(new UserUpdatedDomainEvent(Id, Name, Email?.Value));
    }

    public void Delete()
    {
        // Crear evento solo con los datos que necesitan otros consumidores (Microservicios)
        RemoveDomainEvent(new UserDeletedDomainEvent(Id));
    }

    #region Identification

    public void AddIdentification(Identification identification)
    {
        IdentificationId = identification.Id;
        Identification = identification;
    }

    #endregion

    #region UserAnimal
    public void AddAnimal(UserAnimal userAnimal)
    {
        _userAnimals.Add(userAnimal);
    }

    public void UpdateAnimal(Guid animalId, string newName)
    {
        var userAnimal = _userAnimals.FirstOrDefault(ua => ua.AnimalId == animalId);
        if (userAnimal == null)
        {
            throw new InvalidOperationException("Animal not found for this user.");
        }

        userAnimal.UpdateName(newName);
    }

    public void DeleteAnimal(Guid animalId)
    {
        var userAnimal = _userAnimals.FirstOrDefault(ua => ua.AnimalId == animalId);
        if (userAnimal == null)
        {
            throw new InvalidOperationException("Animal not found for this user.");
        }

        _userAnimals.Remove(userAnimal);
    }

    #endregion

    #region MedicalInformation

    public void AssignMedicalInformation(MedicalInformation medicalInformation)
    {
        MedicalInformationId = medicalInformation.Id;
        MedicalInformation = medicalInformation;
    }

    public void RemoveMedicalInformation()
    {
        MedicalInformationId = null;
        MedicalInformation = null;
    }

    #endregion

    #region PreferredProfessional
    public void AssignPreferredProfessional(PreferredProfessional preferredProfessional)
    {
        PreferredProfessionalId = preferredProfessional.Id;
        PreferredProfessional = preferredProfessional;
    }

    public void RemovePreferredProfessional()
    {
        PreferredProfessionalId = null;
        PreferredProfessional = null;
    }
    #endregion
    
    #region PersonalResource
    public void AddPersonalResource(PersonalResource personalResource)
    {
        _personalResources.Add(personalResource);
    }

    public void UpdatePersonalResource(PersonalResource personalResource)
    {
        var existingPersonalResource = _personalResources.FirstOrDefault(pr => pr.Id == personalResource.Id);
        if (existingPersonalResource == null)
        {
            throw new InvalidOperationException("Personal resource not found for this user.");
        }
        personalResource.Update(personalResource);
    }

    public void DeletePersonalResource(Guid personalResourceId)
    {
        var personalResource = _personalResources.FirstOrDefault(pr => pr.Id == personalResourceId);
        if (personalResource == null)
        {
            throw new InvalidOperationException("Personal resource not found for this user.");
        }
        _personalResources.Remove(personalResource);
    }
    #endregion

    #region WorkCenterResource
    public void AddWorkCenterResource(WorkCenterResource workCenterResource)
    {
        _workCenterResources.Add(workCenterResource);
    }

    public void UpdateWorkCenterResource(WorkCenterResource workCenterResource)
    {
        var existingWorkCenterResource = _workCenterResources.FirstOrDefault(wcr => wcr.Id == workCenterResource.Id);
        if (existingWorkCenterResource == null)
        {
            throw new InvalidOperationException("Work center resource not found for this user.");
        }
        workCenterResource.Update(workCenterResource);
    }

    public void DeleteWorkCenterResource(Guid workCenterResourceId)
    {
        var workCenterResource = _workCenterResources.FirstOrDefault(wcr => wcr.Id == workCenterResourceId);
        if (workCenterResource == null)
        {
            throw new InvalidOperationException("Work center resource not found for this user.");
        }
        _workCenterResources.Remove(workCenterResource);
    }
    #endregion
}
