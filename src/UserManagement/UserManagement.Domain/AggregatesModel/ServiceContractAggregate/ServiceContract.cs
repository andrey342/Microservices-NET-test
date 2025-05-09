using System.Xml.Linq;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;
using UserManagement.Domain.Events;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

public class ServiceContract : Entity, IAggregateRoot
{
    public User User { get; private set; }
    public Guid UserId { get; private set; }
    public WorkCenter WorkCenter { get; private set; }
    public Guid WorkCenterId { get; private set; }
    public ServiceContractStatus CurrentStatus { get; private set; }
    public Guid CurrentStatusId { get; private set; }
    public ServiceType ServiceType { get; private set; }
    public Guid ServiceTypeId { get; private set; }
    public Guid UserTypeId { get; private set; }
    public UserType UserType { get; private set; }
    public Guid UserTypologyId { get; private set; }
    public UserTypology UserTypology { get; private set; }
    private List<Residence> _residences = new List<Residence>();
    public IReadOnlyCollection<Residence> Residences => _residences.AsReadOnly();
    private List<ServiceContractStatusHistory> _serviceContractStatusHistories = new List<ServiceContractStatusHistory>();
    public IReadOnlyCollection<ServiceContractStatusHistory> ServiceContractStatusHistories => _serviceContractStatusHistories.AsReadOnly();
    private List<ServiceContractCentralUnit> _serviceContractCentralUnits = new List<ServiceContractCentralUnit>();
    public IReadOnlyCollection<ServiceContractCentralUnit> ServiceContractCentralUnits => _serviceContractCentralUnits;
    private List<ServiceContractBeneficiary> _beneficiaries = new List<ServiceContractBeneficiary>();
    public IReadOnlyCollection<ServiceContractBeneficiary> Beneficiaries => _beneficiaries.AsReadOnly();

    public ServiceContract() {}

    public ServiceContract(ServiceContract serviceContract)
    {
        this.CopyPropertiesTo(serviceContract);
    }

    #region User
    public void Update(ServiceContract serviceContract)
    {
        // EVITA Copiar propiedad por propiedad (CopyPropertiesFast para +10 propiedades)
        this.CopyPropertiesTo(serviceContract);
    }

    public void AddUser(User user)
    {
        User = user;
        UserId = user.Id;
    }

    #endregion

    #region Residence
    public void AddResidence(Residence residence)
    {
        _residences.Add(residence);
        if (residence.IsCurrentResidence)
        {
            AddDomainEvent(new UserHistoryDomainEvent(
                UserHistoryConstants.Types.Residence,
                UserHistoryConstants.Actions.New,
                $"Residencia actual {residence.GetFullAddress()}",
                UserId,
                Id
            ));
        }
    }

    public void RemoveResidence(Guid residenceId)
    {
        var residence = _residences.FirstOrDefault(r => r.Id == residenceId);
        if (residence == null)
            throw new InvalidOperationException("Residence not found in this serviceContract.");
        _residences.Remove(residence);

        AddDomainEvent(new UserHistoryDomainEvent(
            UserHistoryConstants.Types.Contract,
            UserHistoryConstants.Actions.Delete,
            $"Residencia borrada {residence.GetFullAddress()}",
            UserId,
            Id
        ));
    }

    public void SetCurrentResidence(Guid residenceId)
    {
        var residence = _residences.FirstOrDefault(r => r.Id == residenceId);
        if (residence == null)
            throw new InvalidOperationException("Residence not found in this serviceContract.");

        // Poner a false IsCurrentResidence para todas las residencias
        foreach (var res in _residences)
        {
            res.setCurrentResidence(false);
        }

        // Poner a true IsCurrentResidence para la residencia recibida
        residence.setCurrentResidence(true);

        AddDomainEvent(new UserHistoryDomainEvent(
            UserHistoryConstants.Types.Residence,
            UserHistoryConstants.Actions.Update, 
            $"Residencia actual {residence.GetFullAddress()}", 
            UserId, 
            Id
        ));
    }
    #endregion

    #region Status
    public void AddServiceContractStatusHistory(ServiceContractStatus serviceContractStatus, ServiceContractStatusReason serviceContractStatusReason) 
    {
        var historyEntry = new ServiceContractStatusHistory(this.Id, serviceContractStatus.Id, serviceContractStatusReason.Id, DateTime.UtcNow);

        //Añadir cambio de estado a historico de estados de contrato
        _serviceContractStatusHistories.Add(historyEntry);

        //Actualizar el estado actual
        CurrentStatusId = serviceContractStatus.Id;
        CurrentStatus = serviceContractStatus;

        //Dominio de evento añadir a historico de usuario
        AddDomainEvent(new UserHistoryDomainEvent(
            UserHistoryConstants.Types.Contract, 
            UserHistoryConstants.Actions.Update, 
            $"Estado actual: {serviceContractStatus.Name}, motivo: {serviceContractStatusReason.Name}", 
            UserId, 
            Id
        ));
    }
    #endregion

    #region Central Unit
    public void AddServiceContractCentralUnit(ServiceContractCentralUnit serviceContractCentralUnit)
    {
        _serviceContractCentralUnits.Add(serviceContractCentralUnit);
        //Dominio de evento añadir a historico de usuario
        AddDomainEvent(new UserHistoryDomainEvent(
            UserHistoryConstants.Types.Device,
            UserHistoryConstants.Actions.New,
            $"Asignada unidad central {serviceContractCentralUnit.CentralUnitId}",
            UserId,
            Id
        ));
    }

    public void RemoveServiceContractCentralUnit(Guid serviceContractCentralUnitId)
    {
        var serviceContractCentralUnit = _serviceContractCentralUnits.FirstOrDefault(cu => cu.Id == serviceContractCentralUnitId);
        if (serviceContractCentralUnit == null)
            throw new InvalidOperationException("Central Unit not found in this serviceContract.");
        _serviceContractCentralUnits.Remove(serviceContractCentralUnit);
        AddDomainEvent(new UserHistoryDomainEvent(
            UserHistoryConstants.Types.Device,
            UserHistoryConstants.Actions.Delete,
            $"Retirada unidad central {serviceContractCentralUnit.CentralUnitId}",
            UserId,
            Id
        ));
    }
    #endregion

    #region Beneficiary
    public void AddBeneficiary(ServiceContractBeneficiary serviceContractBeneficiary)
    {
        _beneficiaries.Add(serviceContractBeneficiary);
        AddDomainEvent(new UserHistoryDomainEvent(
            UserHistoryConstants.Types.Beneficiary,
            UserHistoryConstants.Actions.New,
            $"Añadido beneficiario {serviceContractBeneficiary.UserId}",
            UserId,
            Id
        ));
    }

    public void RemoveBeneficiary(Guid serviceContractBeneficiaryId)
    {
        var serviceContractBeneficiary = _beneficiaries.FirstOrDefault(b => b.Id == serviceContractBeneficiaryId);
        if (serviceContractBeneficiary == null)
            throw new InvalidOperationException("Beneficiary not found in this serviceContract.");
        _beneficiaries.Remove(serviceContractBeneficiary);
        AddDomainEvent(new UserHistoryDomainEvent(
            UserHistoryConstants.Types.Beneficiary,
            UserHistoryConstants.Actions.Delete,
            $"Eliminado beneficiario {serviceContractBeneficiary.UserId}",
            UserId,
            Id
        ));
    }
    #endregion
}