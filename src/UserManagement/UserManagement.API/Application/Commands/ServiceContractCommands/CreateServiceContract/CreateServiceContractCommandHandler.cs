using UserManagement.Domain.Repositories;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.API.Application.Commands.UserCommands.CreateUser;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.CreateServiceContract;

public class CreateServiceContractCommandHandler : IRequestHandler<CreateServiceContractCommand, Result<CreatedContractViewModel>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateServiceContractCommandHandler(IServiceContractRepository serviceContractRepository, IUserRepository userRepository, IMapper mapper, IMediator mediator)
    {
        _serviceContractRepository = serviceContractRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<CreatedContractViewModel>> Handle(CreateServiceContractCommand request, CancellationToken cancellationToken)
    {
        var serviceContract = _mapper.Map<ServiceContract>(request.ServiceContractRequest);
        User user;

        if (request.ServiceContractRequest.UserId.HasValue)
        {
            // Case 1: Existing user
            user = await _userRepository.GetByIdAsync(request.ServiceContractRequest.UserId.Value);
            if (user == null)
            {
                return Result<CreatedContractViewModel>.FailureResult("User not found.");
            }
        }
        else if (request.ServiceContractRequest.UserRequest != null)
        {
            // Case 2: Create user first
            var createUserCommand = new CreateUserCommand(request.ServiceContractRequest.UserRequest);

            var createUserResult = await _mediator.Send(createUserCommand, cancellationToken);
            if (!createUserResult.Success)
            {
                return Result<CreatedContractViewModel>.FailureResult("Error creating user.");
            }

            user = await _userRepository.GetByIdAsync(createUserResult.Data);
        }
        else
        {
            // Case 3: Neither UserId nor UserRequest provided
            return Result<CreatedContractViewModel>.FailureResult("UserId or UserRequest must be provided.");
        }
        serviceContract.AddUser(user);

        // Add default serviceContract status to the serviceContract
        var serviceContractStatus = await _serviceContractRepository.GetDefaultServiceContractStatusAsync();
        var serviceContractStatusReason = await _serviceContractRepository.GetDefaultServiceContractStatusReasonAsync();

        if (serviceContractStatus != null && serviceContractStatusReason != null)
        {
            serviceContract.AddServiceContractStatusHistory(serviceContractStatus, serviceContractStatusReason);
        }

        _serviceContractRepository.Add(serviceContract);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var serviceContractDto = _mapper.Map<FullServiceContractViewModel>(serviceContract);

        var result = new CreatedContractViewModel
        {
            ContractId = serviceContractDto.Id,
            UserId = serviceContractDto.UserId
        };
        return Result<CreatedContractViewModel>.SuccessResult(result, "ServiceContract created successfully.");
    }
}