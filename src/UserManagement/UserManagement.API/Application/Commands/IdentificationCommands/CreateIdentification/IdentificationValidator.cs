using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

namespace UserManagement.API.Application.Commands.IdentificationCommands.CreateIdentification;

public class IdentificationValidator : BaseValidator<CreateIdentificationRequest>
{
    public IdentificationValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Campos obligatorios
        ValidateString(x => x.Number, 20, isRequired: true);
        ValidateGuid<IdentificationType>(x => x.TypeId, isRequired: true);
        // Opcionales
        ValidateGuid<Identification>(x => x.Id);
        ValidateDate(x => x.ExpirationDate, isFutureDate: true);
        ValidateDate(x => x.UpdateDate, isFutureDate: true);
    }
}
