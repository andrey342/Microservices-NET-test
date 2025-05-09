using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.ResidenceCommands.DeleteResidence
{
    public class DeleteResidenceCommandValidator : BaseValidator<DeleteResidenceCommand>
    {
        public DeleteResidenceCommandValidator(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
            ValidateGuid<Residence>(x => x.Id, isRequired: true);
        }
    }
}