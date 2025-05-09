using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserAnimalCommands.CreateUserAnimal;

public class CreateUserAnimalCommandValidator : BaseValidator<CreateUserAnimalCommand>
{
    public CreateUserAnimalCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
    {
        // Obligatorios
        ValidateString(x => x.UserAnimalCreateDto.Name, 50, isRequired: true);
        ValidateGuid<User>(x => x.UserAnimalCreateDto.UserId, isRequired: true);
        ValidateGuid<Animal>(x => x.UserAnimalCreateDto.AnimalId, isRequired: true);
    }
}
