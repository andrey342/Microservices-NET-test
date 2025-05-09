using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.ResidenceCommands.CreateResidence;
public class CreateResidenceCommandValidator : BaseValidator<CreateResidenceCommand>
{
    public CreateResidenceCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<ServiceContract>(x => x.AddResidenceToServiceContractRequest.ServiceContractId, isRequired: true);
        ValidateString(x => x.AddResidenceToServiceContractRequest.Address.RoadType, maxLength: 100, isRequired: true);
        ValidateString(x => x.AddResidenceToServiceContractRequest.Address.StreetName, maxLength: 100, isRequired: true);
        ValidateString(x => x.AddResidenceToServiceContractRequest.Address.PostalCode, maxLength: 20, isRequired: true);
        ValidateString(x => x.AddResidenceToServiceContractRequest.Address.Town, maxLength: 100, isRequired: true);
        ValidateString(x => x.AddResidenceToServiceContractRequest.Address.Province, maxLength: 100, isRequired: true);
        ValidateString(x => x.AddResidenceToServiceContractRequest.Address.Door, maxLength: 10, isRequired: false);
        ValidateString(x => x.AddResidenceToServiceContractRequest.Address.Floor, maxLength: 10, isRequired: false);
        ValidateString(x => x.AddResidenceToServiceContractRequest.Address.Number, maxLength: 10, isRequired: false);
        ValidateString(x => x.AddResidenceToServiceContractRequest.Address.Stair, maxLength: 10, isRequired: false);
        ValidateDecimal(x => x.AddResidenceToServiceContractRequest.Address.Latitude, isRequired: false, precision: 9, scale: 6);
        ValidateDecimal(x => x.AddResidenceToServiceContractRequest.Address.Longitude, isRequired: false, precision: 9, scale: 6);
        ValidateBoolean(x => x.AddResidenceToServiceContractRequest.Elevator, isRequired: true);
        ValidateBoolean(x => x.AddResidenceToServiceContractRequest.Concierge, isRequired: true);
        ValidateBoolean(x => x.AddResidenceToServiceContractRequest.Doorman, isRequired: true);
        ValidateBoolean(x => x.AddResidenceToServiceContractRequest.FireHydrant, isRequired: true);
        ValidateBoolean(x => x.AddResidenceToServiceContractRequest.Wifi, isRequired: true);
        ValidateBoolean(x => x.AddResidenceToServiceContractRequest.Gas, isRequired: true);
        ValidateBoolean(x => x.AddResidenceToServiceContractRequest.Electricity, isRequired: true);
        ValidateBoolean(x => x.AddResidenceToServiceContractRequest.Water, isRequired: true);
        ValidateBoolean(x => x.AddResidenceToServiceContractRequest.Internet, isRequired: true);
        ValidateString(x => x.AddResidenceToServiceContractRequest.ArchitecturalBarrierEntrance, maxLength: 100, isRequired: false);
        ValidateString(x => x.AddResidenceToServiceContractRequest.ArchitecturalBarriereResidence, maxLength: 100, isRequired: false);
        ValidateString(x => x.AddResidenceToServiceContractRequest.Observation, maxLength: 500, isRequired: false);
        ValidateBoolean(x => x.AddResidenceToServiceContractRequest.IsCurrentResidence, isRequired: true);
    }
}