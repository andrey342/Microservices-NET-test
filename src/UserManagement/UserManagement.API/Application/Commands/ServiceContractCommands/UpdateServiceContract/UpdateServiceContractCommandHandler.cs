using UserManagement.API.Application.Commands.UserCommands.CreateUser;
using UserManagement.API.Application.Commands.UserCommands.UpdateUser;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContract;

public class UpdateServiceContractCommandHandler : IRequestHandler<UpdateServiceContractCommand, Result<BasicServiceContractViewModel>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateServiceContractCommandHandler(IServiceContractRepository serviceContractRepository, IUserRepository userRepository, IMapper mapper, IMediator mediator)
    {
        _serviceContractRepository = serviceContractRepository;
        _mapper = mapper;
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public async Task<Result<BasicServiceContractViewModel>> Handle(UpdateServiceContractCommand request, CancellationToken cancellationToken)
    {
        var existingServiceContract = await _serviceContractRepository.GetByIdAsync(request.ServiceContractRequest.Id);

        //Actualizar serviceContract
        existingServiceContract.Update(_mapper.Map<ServiceContract>(request.ServiceContractRequest));

        //Añadir nuevo estado y motivo al historico de estados del contrato
        if (request.ServiceContractRequest.ServiceContractStatusReasonId.HasValue)
        {
            var serviceContractStatusReason = await _serviceContractRepository.GetServiceContractStatusReasonByIdAsync(request.ServiceContractRequest.ServiceContractStatusReasonId.Value);
            if (serviceContractStatusReason == null)
            {
                return Result<BasicServiceContractViewModel>.FailureResult("Service contract status reason not found.");
            }
            var serviceContractStatus = await _serviceContractRepository.GetServiceContractStatusByIdAsync(request.ServiceContractRequest.CurrentStatusId);

            existingServiceContract.AddServiceContractStatusHistory(serviceContractStatus, serviceContractStatusReason);
        }

        //Actualizar usuario a traves de command
        if (request.ServiceContractRequest.UserRequest != null)
        {
            var updateUserCommand = new UpdateUserCommand(request.ServiceContractRequest.UserRequest);
            var updateUserResult = await _mediator.Send(updateUserCommand, cancellationToken);
            if (!updateUserResult.Success)
            {
                return Result<BasicServiceContractViewModel>.FailureResult("Error updating user.");
            }
        }

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var serviceContractViewModel = _mapper.Map<BasicServiceContractViewModel>(existingServiceContract);

        return Result<BasicServiceContractViewModel>.SuccessResult(serviceContractViewModel, "Service contract updated successfully.");
    }
}
