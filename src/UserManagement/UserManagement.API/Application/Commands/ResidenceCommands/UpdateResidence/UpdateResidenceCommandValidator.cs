using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.ResidenceCommands.UpdateResidence;

public class UpdateResidenceCommandValidator : BaseValidator<UpdateResidenceCommand>
{
    public UpdateResidenceCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<Residence>(x => x.UpdateResidenceInServiceContractRequest.Id, isRequired: true);
        ValidateGuid<ServiceContract>(x => x.UpdateResidenceInServiceContractRequest.ServiceContractId, isRequired: true);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.Address.RoadType, maxLength: 100, isRequired: true);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.Address.StreetName, maxLength: 100, isRequired: true);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.Address.PostalCode, maxLength: 20, isRequired: true);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.Address.Town, maxLength: 100, isRequired: true);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.Address.Province, maxLength: 100, isRequired: true);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.Address.Door, maxLength: 10, isRequired: false);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.Address.Floor, maxLength: 10, isRequired: false);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.Address.Number, maxLength: 10, isRequired: false);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.Address.Stair, maxLength: 10, isRequired: false);
        ValidateDecimal(x => x.UpdateResidenceInServiceContractRequest.Address.Latitude, isRequired: false, precision: 9, scale: 6);
        ValidateDecimal(x => x.UpdateResidenceInServiceContractRequest.Address.Longitude, isRequired: false, precision: 9, scale: 6);
        ValidateBoolean(x => x.UpdateResidenceInServiceContractRequest.Elevator, isRequired: true);
        ValidateBoolean(x => x.UpdateResidenceInServiceContractRequest.Concierge, isRequired: true);
        ValidateBoolean(x => x.UpdateResidenceInServiceContractRequest.Doorman, isRequired: true);
        ValidateBoolean(x => x.UpdateResidenceInServiceContractRequest.FireHydrant, isRequired: true);
        ValidateBoolean(x => x.UpdateResidenceInServiceContractRequest.Wifi, isRequired: true);
        ValidateBoolean(x => x.UpdateResidenceInServiceContractRequest.Gas, isRequired: true);
        ValidateBoolean(x => x.UpdateResidenceInServiceContractRequest.Electricity, isRequired: true);
        ValidateBoolean(x => x.UpdateResidenceInServiceContractRequest.Water, isRequired: true);
        ValidateBoolean(x => x.UpdateResidenceInServiceContractRequest.Internet, isRequired: true);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.ArchitecturalBarrierEntrance, maxLength: 100, isRequired: false);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.ArchitecturalBarriereResidence, maxLength: 100, isRequired: false);
        ValidateString(x => x.UpdateResidenceInServiceContractRequest.Observation, maxLength: 500, isRequired: false);
        ValidateBoolean(x => x.UpdateResidenceInServiceContractRequest.IsCurrentResidence, isRequired: true);

    }
}