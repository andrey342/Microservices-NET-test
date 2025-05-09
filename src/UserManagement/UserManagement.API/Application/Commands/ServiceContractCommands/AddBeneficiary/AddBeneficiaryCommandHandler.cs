using UserManagement.API.Application.Commands.UserCommands.CreateUser;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddBeneficiary;

public class AddBeneficiaryCommandHandler : IRequestHandler<AddBeneficiaryCommand, Result<Guid>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AddBeneficiaryCommandHandler(IServiceContractRepository serviceContractRepository, IUserRepository userRepository, IMapper mapper, IMediator mediator)
    {
        _serviceContractRepository = serviceContractRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<Guid>> Handle(AddBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var serviceContract = await _serviceContractRepository.GetByIdAsync(request.BeneficiaryRequest.ServiceContractId);
        User user;

        if (request.BeneficiaryRequest.UserId.HasValue && serviceContract.UserId == request.BeneficiaryRequest.UserId)
        {
            return Result<Guid>.FailureResult("Owner cannot be a beneficiary.");
        }

        if (request.BeneficiaryRequest.UserId.HasValue)
        {
            // Case 1: Existing user
            user = await _userRepository.GetByIdAsync(request.BeneficiaryRequest.UserId.Value);
            if (user == null)
            {
                return Result<Guid>.FailureResult("User not found.");
            }
        }
        else if (request.BeneficiaryRequest.User != null)
        {
            // Case 2: Create user first
            var createUserCommand = new CreateUserCommand(request.BeneficiaryRequest.User);

            var createUserResult = await _mediator.Send(createUserCommand, cancellationToken);
            if (!createUserResult.Success)
            {
                return Result<Guid>.FailureResult("Error creating user.");
            }

            user = await _userRepository.GetByIdAsync(createUserResult.Data);
        }
        else
        {
            // Case 3: Neither UserId nor UserRequest provided
            return Result<Guid>.FailureResult("UserId or UserRequest must be provided.");
        }

        request.BeneficiaryRequest.UserId = user.Id;
        request.BeneficiaryRequest.User = null;
        var serviceContractBeneficiary = _mapper.Map<ServiceContractBeneficiary>(request.BeneficiaryRequest);

        serviceContract.AddBeneficiary(serviceContractBeneficiary);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(serviceContractBeneficiary.UserId);
    }
}

