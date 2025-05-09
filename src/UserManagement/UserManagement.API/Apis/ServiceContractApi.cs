using Microsoft.AspNetCore.Http.HttpResults;
using UserManagement.API.Application.Commands.CohabitantCommands.CreateCohabitant;
using UserManagement.API.Application.Commands.CohabitantCommands.DeleteCohabitant;
using UserManagement.API.Application.Commands.CohabitantCommands.UpdateCohabitant;
using UserManagement.API.Application.Commands.ServiceContractCommands.CreateServiceContract;
using UserManagement.API.Application.Commands.IdentifiedCommands;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;
using UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContractStatus;
using UserManagement.API.Application.Commands.ServiceContractCommands.UpdateCurrentResidence;
using UserManagement.API.Application.Commands.ResidenceCommands.CreateResidence;
using UserManagement.API.Application.Commands.ResidenceCommands.UpdateResidence;
using UserManagement.API.Application.Commands.ResidenceCommands.DeleteResidence;
using UserManagement.API.Application.Commands.ServiceContractCommands.AddCentralUnit;
using UserManagement.API.Application.Commands.ServiceContractCommands.AddPeripheral;
using UserManagement.API.Application.Commands.ServiceContractCommands.RemoveCentralUnit;
using UserManagement.API.Application.Commands.ServiceContractCommands.RemovePeripheral;
using UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContract;
using UserManagement.API.Application.Commands.KeyCommands.CreateKey;
using UserManagement.API.Application.Commands.KeyCommands.UpdateKey;
using UserManagement.API.Application.Commands.KeyCommands.DeleteKey;
using UserManagement.API.Application.Commands.ServiceContractCommands.AddBeneficiary;
using UserManagement.API.Application.Commands.ServiceContractCommands.RemoveBeneficiary;
using UserManagement.API.Application.Queries.CentralUnitQueries;
using UserManagement.API.Application.Queries.PeripheralQueries;
using UserManagement.API.Application.Commands.CentralUnitCommands.UpdateCentralUnit;
using UserManagement.API.Application.Commands.PeripheralCommands.UpdatePeripheral;
using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Apis;

public static class ServiceContractApi
{
    public static RouteGroupBuilder MapServiceContractsApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("serviceContract").HasApiVersion(1.0);

        #region Apis principales

        api.MapGet("/getById/{id:Guid}", GetServiceContractByIdAsync);
        api.MapGet("/getServiceContractsByWorkCenter/{workCenterId:Guid}", GetServiceContractsByWorkCenterAsync);
        api.MapGet("/getAllUsersByWorkCenter/{workCenterId:Guid}", GetAllUsersByWorkCenterAsync);
        api.MapGet("/getUserWithContractsByUserId/{userId:Guid}", GetUserWithContractsByUserIdAsync);
        api.MapGet("/getResidence/{residenceId:Guid}", GetResidenceByIdAsync);
        api.MapGet("/getResidencesByServiceContract/{serviceContractId:Guid}", GetResidencesByServiceContractIdAsync);
        api.MapGet("/getRoadTypes", GetRoadTypes);
        api.MapGet("/getProvinces", GetProvinces);
        api.MapGet("/getCohabitant/{cohabitantId:Guid}", GetCohabitantByIdAsync);
        api.MapGet("/getCohabitantsByResidence/{residenceId:Guid}", GetCohabitantsByResidenceIdAsync);
        api.MapGet("/getKeysByResidence/{residenceId:Guid}", GetKeysByResidenceIdAsync);
        api.MapGet("/getReasonsByStatusId/{statusId:Guid}", GetStatusReasonByStatusIdAsync);
        api.MapGet("/getAvailableServiceTypes/{userId:Guid}", GetAvailableServiceTypesAsync);
        api.MapGet("/getCentralUnitBySerialNumber/{serialNumber}", GetCentralUnitBySerialNumberAsync);
        api.MapGet("/getPeripheralBySerialNumber/{serialNumber}", GetPeripheralBySerialNumberAsync);

        api.MapPost("/create", CreateServiceContractAsync);
        api.MapPut("/update", UpdateServiceContractAsync);
        api.MapPut("/updateCurrentStatus", UpdateServiceContractStatusAsync);

        api.MapPost("/createCohabitant", CreateCohabitantAsync);
        api.MapPut("/updateCohabitant", UpdateCohabitantAsync);
        api.MapDelete("/deleteCohabitant/{id:Guid}", DeleteCohabitantAsync);

        api.MapPost("/createKey", CreateKeyAsync);
        api.MapPut("/updateKey", UpdateKeyAsync);
        api.MapDelete("/deleteKey/{id:Guid}", DeleteKeyAsync);
        api.MapGet("/getKeyHistory/{keyId:Guid}", GetKeyHistoryByKeyIdAsync);

        api.MapPost("/createResidence", CreateResidenceAsync);
        api.MapPut("/updateResidence", UpdateResidenceAsync);
        api.MapPut("/updateCurrentResidence", UpdateCurrentResidenceAsync);
        api.MapDelete("/deleteResidence/{id:Guid}", DeleteResidenceAsync);

        api.MapPost("/addCentralUnit", AddCentralUnitAsync);
        api.MapPut("/updateCentralUnit", UpdateCentralUnitAsync);
        api.MapDelete("/removeCentralUnit/{id:Guid}", RemoveCentralUnitAsync);
        api.MapPost("/addPeripheral", AddPeripheralAsync);
        api.MapPut("/updatePeripheral", UpdatePeripheralAsync);
        api.MapDelete("/removePeripheral/{id:Guid}", RemovePeripheralAsync);
        api.MapGet("/getCentralUnits/{serviceContractId:Guid}", GetCentralUnitByServiceContractIdAsync);
        api.MapGet("/getPeripherals/{centralUnitId:Guid}", GetPeripheralsByCentralUnitIdAsync);

        api.MapPost("/addBeneficiary", AddBeneficiaryAsync);
        api.MapDelete("/removeBeneficiary/{id:Guid}", RemoveBeneficiaryAsync);
        api.MapGet("/getBeneficiariesByServiceContract/{serviceContractId:Guid}", GetBeneficiariesByServiceContractAsync);
        api.MapGet("/getServiceContractBeneficiary", GetServiceContractBeneficiaryAsync);

        api.MapGet("/getUsersByContract/{serviceContractId:Guid}", GetUsersByContractAsync);
        #endregion

        #region Maestros

        // Lista de maestros con su tipo de Entidad y su ViewModel
        var masters = new List<(string Route, Type EntityType, Type ViewModelType)>
        {
            ("cohabitantTypes", typeof(CohabitantType), typeof(CohabitantTypeViewModel)),
            ("workCenters", typeof(WorkCenter), typeof(WorkCenterUmViewModel)),
            ("serviceTypes", typeof(ServiceType), typeof(ServiceTypeViewModel)),
            ("serviceContractStatuses", typeof(ServiceContractStatus), typeof(ServiceContractStatusViewModel)),
            ("keyStatuses", typeof(KeyStatus), typeof(KeyStatusViewModel)),
        };

        foreach (var (route, entityType, viewModelType) in masters)
        {
            // Utilizamos un método genérico para definir los endpoints con el tipo correcto
            typeof(ServiceContractApi)
                .GetMethod(nameof(RegisterMasterEndpoint), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?
                .MakeGenericMethod(entityType, viewModelType)
                .Invoke(null, new object[] { api, route });
        }

        #endregion

        return api;
    }

    #region Master rute generator

    // Método genérico que registra cada endpoint asegurando que tenga la documentación correcta en Swagger
    private static void RegisterMasterEndpoint<TMaster, TViewModel>(RouteGroupBuilder api, string route)
        where TMaster : class
        where TViewModel : class
    {
        api.MapGet($"/{route}", async ([FromServices] IServiceContractQueries queries) =>
            TypedResults.Ok(await queries.GetAllMastersAsync<TMaster, TViewModel>())
        )
        .Produces<IEnumerable<TViewModel>>(StatusCodes.Status200OK) // Define la respuesta en Swagger
        .WithName($"Get{typeof(TViewModel).Name}") // Nombre único en Swagger
        .WithTags("Masters"); // Categoría en Swagger
    }

    #endregion

    #region Queries

    /// <summary>
    /// Obtiene un contrato de servicio por su ID.
    /// </summary>
    /// <param name="id">ID del contrato de servicio.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>El contrato de servicio correspondiente al ID proporcionado.</returns>
    public static async Task<Results<Ok<FullServiceContractViewModel>, NotFound<string>>> GetServiceContractByIdAsync(
      Guid id, [FromServices] IServiceContractQueries queries)
    {
        try
        {
            var serviceContract = await queries.GetServiceContractByIdAsync(id);
            return TypedResults.Ok(serviceContract);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene todos los contratos de servicio de un centro de trabajo específico.
    /// </summary>
    /// <param name="workCenterId">ID del centro de trabajo.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>Una lista de contratos de servicio correspondientes al centro de trabajo proporcionado.</returns>
    public static async Task<Ok<IEnumerable<FullServiceContractViewModel>>> GetServiceContractsByWorkCenterAsync(
      Guid workCenterId, [FromServices] IServiceContractQueries queries)
    {
        var serviceContracts = await queries.GetServiceContractsByWorkCenterAsync(workCenterId);
        return TypedResults.Ok(serviceContracts);
    }

    /// <summary>
    /// Obtiene todos los usuarios con contratos de un centro de trabajo específico.
    /// </summary>
    /// <param name="workCenterId">ID del centro de trabajo.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>Una lista de usuarios con contratos correspondientes al centro de trabajo proporcionado.</returns>
    public static async Task<Ok<IEnumerable<BasicUserWithContractsViewModel>>> GetAllUsersByWorkCenterAsync(
    Guid workCenterId,
    [FromServices] IServiceContractQueries queries)
    {
        var result = await queries.GetAllUsersContractByWorkcenterIdAsync(workCenterId);
        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Obtiene todas las residencias asociadas a un contrato de servicio específico.
    /// </summary>
    /// <param name="serviceContractId">ID del contrato de servicio.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>Una lista de residencias correspondientes al contrato de servicio proporcionado.</returns>
    public static async Task<Ok<IEnumerable<ResidenceViewModel>>> GetResidencesByServiceContractIdAsync(
        Guid serviceContractId, [FromServices] IServiceContractQueries queries)
    {
        var residences = await queries.GetResidencesByServiceContractIdAsync(serviceContractId);
        return TypedResults.Ok(residences);
    }

    /// <summary>
    /// Obtiene una residencia por su ID.
    /// </summary>
    /// <param name="residenceId">ID de la residencia.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>La residencia correspondiente al ID proporcionado.</returns>
    public static async Task<Results<Ok<ResidenceViewModel>, NotFound<string>>> GetResidenceByIdAsync(
        Guid residenceId, [FromServices] IServiceContractQueries queries)
    {
        try
        {
            var residence = await queries.GetResidenceByIdAsync(residenceId);
            return TypedResults.Ok(residence);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene todos los cohabitantes de una residencia específica.
    /// </summary>
    /// <param name="residenceId">ID de la residencia.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>Una lista de cohabitantes correspondientes a la residencia proporcionada.</returns>
    public static async Task<Ok<IEnumerable<CohabitantViewModel>>> GetCohabitantsByResidenceIdAsync(
        Guid residenceId, [FromServices] IServiceContractQueries queries)
    {
        var cohabitants = await queries.GetCohabitantsByResidenceIdAsync(residenceId);
        return TypedResults.Ok(cohabitants);
    }

    /// <summary>
    /// Obtiene todas las llaves asociadas a una residencia específica.
    /// </summary>
    /// <param name="residenceId">ID de la residencia.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>Una lista de llaves correspondientes a la residencia proporcionada.</returns>
    public static async Task<Ok<IEnumerable<KeyViewModel>>> GetKeysByResidenceIdAsync(
        Guid residenceId, [FromServices] IServiceContractQueries queries)
    {
        var keys = await queries.GetKeysByResidenceIdAsync(residenceId);
        return TypedResults.Ok(keys);
    }

    /// <summary>
    /// Obtiene un cohabitante por su ID.
    /// </summary>
    /// <param name="cohabitantId">ID del cohabitante.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>El cohabitante correspondiente al ID proporcionado.</returns>
    public static async Task<Results<Ok<CohabitantViewModel>, NotFound<string>>> GetCohabitantByIdAsync(
        Guid cohabitantId, [FromServices] IServiceContractQueries queries)
    {
        try
        {
            var cohabitant = await queries.GetCohabitantByIdAsync(cohabitantId);
            return TypedResults.Ok(cohabitant);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene un usuario con contratos por su ID.
    /// </summary>
    /// <param name="userId">ID del usuario.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>El usuario con contratos correspondiente al ID proporcionado.</returns>
    public static async Task<Results<Ok<FullUserWithContractsViewModel>, NotFound<string>>> GetUserWithContractsByUserIdAsync(
        Guid userId, [FromServices] IServiceContractQueries queries)
    {
        try
        {
            var residence = await queries.GetUserWithContractsByUserIdAsync(userId);
            return TypedResults.Ok(residence);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene las razones de estado por el ID del estado.
    /// </summary>
    /// <param name="statusId">ID del estado.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>Una lista de razones de estado correspondientes al ID del estado proporcionado.</returns>
    public static async Task<Ok<IEnumerable<ServiceContractStatusReasonViewModel>>> GetStatusReasonByStatusIdAsync(
    Guid statusId,
    [FromServices] IServiceContractQueries queries)
    {
        var reasons = await queries.GetStatusReasonByStatusIdAsync(statusId);
        return TypedResults.Ok(reasons);
    }

    public static async Task<Ok<IEnumerable<ServiceTypeViewModel>>> GetAvailableServiceTypesAsync(
    Guid userId,
    [FromServices] IServiceContractQueries queries)
    {
        var serviceTypes = await queries.GetAvailableServiceTypesAsync(userId);
        return TypedResults.Ok(serviceTypes);
    }

    public static async Task<Ok<IEnumerable<string>>> GetRoadTypes()
    {
        var roadTypes = RoadType.GetRoadTypes();
        return TypedResults.Ok(roadTypes);
    }

    public static async Task<Ok<IEnumerable<string>>> GetProvinces()
    {
        var provinces = Province.GetProvinces();
        return TypedResults.Ok(provinces);
    }

    public static async Task<Ok<IEnumerable<ServiceContractCentralUnitViewModel>>> GetCentralUnitByServiceContractIdAsync(
    Guid serviceContractId, [FromServices] IServiceContractQueries queries)
    {
        var centralUnits = await queries.GetCentralUnitByServiceContractIdAsync(serviceContractId);
        return TypedResults.Ok(centralUnits);
    }

    public static async Task<Ok<IEnumerable<PeripheralViewModel>>> GetPeripheralsByCentralUnitIdAsync(
    Guid centralUnitId, [FromServices] IServiceContractQueries queries)
    {
        var data = await queries.GetPeripheralsByCentralUnitIdAsync(centralUnitId);
        return TypedResults.Ok(data);
    }

    /// <summary>
    /// Obtiene una unidad central por su número de serie.
    /// </summary>
    /// <param name="serialNumber">Número de serie de la unidad central.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>La unidad central correspondiente al número de serie proporcionado.</returns>
    public static async Task<Results<Ok<CentralUnitViewModel>, NotFound<string>>> GetCentralUnitBySerialNumberAsync(
        string serialNumber, [FromServices] IServiceContractQueries queries)
    {
        try
        {
            var centralUnit = await queries.GetCentralUnitBySerialNumberAsync(serialNumber);
            return TypedResults.Ok(centralUnit);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene un periférico por su número de serie.
    /// </summary>
    /// <param name="serialNumber">Número de serie del periférico.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>El periférico correspondiente al número de serie proporcionado.</returns>
    public static async Task<Results<Ok<PeripheralViewModel>, NotFound<string>>> GetPeripheralBySerialNumberAsync(
        string serialNumber, [FromServices] IServiceContractQueries queries)
    {
        try
        {
            var peripheral = await queries.GetPeripheralBySerialNumberAsync(serialNumber);
            return TypedResults.Ok(peripheral);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    public static async Task<Ok<IEnumerable<KeyHistoryViewModel>>> GetKeyHistoryByKeyIdAsync(
    Guid keyId, [FromServices] IServiceContractQueries queries)
    {
        var data = await queries.GetKeyHistoryByKeyIdAsync(keyId);
        return TypedResults.Ok(data);
    }

    /// <summary>
    /// Obtiene los beneficiarios de un contrato de servicio específico.
    /// </summary>
    /// <param name="serviceContractId">ID del contrato de servicio.</param>
    /// <param name="queries">Consultas de contrato de servicio.</param>
    /// <returns>Una lista de beneficiarios correspondientes al contrato de servicio proporcionado.</returns>
    public static async Task<Ok<IEnumerable<BasicUserViewModel>>> GetBeneficiariesByServiceContractAsync(
        Guid serviceContractId, [FromServices] IServiceContractQueries queries)
    {
        var beneficiaries = await queries.GetBeneficiariesByServiceContractAsync(serviceContractId);
        return TypedResults.Ok(beneficiaries);
    }

    public static async Task<Results<Ok<FullServiceContractViewModel>, NotFound<string>>> GetServiceContractBeneficiaryAsync(
    Guid serviceContractId,
    Guid beneficiaryId,
    [FromServices] IServiceContractQueries queries)
    {
        try
        {
            var serviceContract = await queries.GetServiceContractBeneficiaryAsync(serviceContractId, beneficiaryId);
            return TypedResults.Ok(serviceContract);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    public static async Task<Results<Ok<UsersByContractViewModel>, NotFound<string>>> GetUsersByContractAsync(
    Guid serviceContractId,
    [FromServices] IServiceContractQueries queries)
    {
        try
        {
            var vm = await queries.GetUsersByContractAsync(serviceContractId);
            return TypedResults.Ok(vm);
        }
        catch (KeyNotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }
    #endregion

    #region Commands

    /// <summary>
    /// Crea un nuevo contrato de servicio.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de creación de contrato de servicio.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El contrato de servicio creado.</returns>
    public static async Task<Results<Ok<CreatedContractViewModel>, BadRequest<string>, ProblemHttpResult>> CreateServiceContractAsync(
      [FromHeader(Name = "x-requestid")] Guid requestId,
      CreateServiceContractRequest request,
      [AsParameters] UserServices services)
    {

        var createServiceContractCommand = new CreateServiceContractCommand(request);
        var requestCreateServiceContract = new IdentifiedCommand<CreateServiceContractCommand, Result<CreatedContractViewModel>>(createServiceContractCommand, requestId);

        var serviceContractResult = await services.Mediator.Send(requestCreateServiceContract);

        if (!serviceContractResult.Success)
        {
            return TypedResults.Problem(detail: serviceContractResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(serviceContractResult.Data);
    }

    /// <summary>
    /// Actualiza un contrato de servicio existente.
    /// </summary>
    /// <param name="request">Solicitud de actualización de contrato de servicio.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El contrato de servicio actualizado.</returns>
    public static async Task<Results<Ok<BasicServiceContractViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateServiceContractAsync(
      UpdateServiceContractRequest request,
      [AsParameters] UserServices services)
    {
        var updateServiceContractCommand = new UpdateServiceContractCommand(request);
        var result = await services.Mediator.Send(updateServiceContractCommand);
        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }
        return TypedResults.Ok(result.Data);
    }

    /// <summary>
    /// Crea un nuevo cohabitante.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de creación de cohabitante.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El ID del cohabitante creado.</returns>
    public static async Task<Results<Ok<Guid>, BadRequest<string>, ProblemHttpResult>> CreateCohabitantAsync(
    [FromHeader(Name = "x-requestid")] Guid requestId,
    CreateCohabitantRequest request,
    [AsParameters] UserServices services)
    {
        var createCommand = new CreateCohabitantCommand(request);
        var requestCreateCohabitant = new IdentifiedCommand<CreateCohabitantCommand, Result<Guid>>(createCommand, requestId);

        var result = await services.Mediator.Send(requestCreateCohabitant);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok(result.Data);
    }

    /// <summary>
    /// Actualiza un cohabitante existente.
    /// </summary>
    /// <param name="request">Solicitud de actualización de cohabitante.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El cohabitante actualizado.</returns>
    public static async Task<Results<Ok<CohabitantViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateCohabitantAsync(
    UpdateCohabitantRequest request,
    [AsParameters] UserServices services)
    {
        var updateCohabitantCommand = new UpdateCohabitantCommand(request);
        var result = await services.Mediator.Send(updateCohabitantCommand);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok(result.Data);
    }

    /// <summary>
    /// Elimina un cohabitante por su ID.
    /// </summary>
    /// <param name="cohabitantId">ID del cohabitante.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>Resultado de la eliminación.</returns>
    public static async Task<Results<Ok, NotFound<string>, ProblemHttpResult>> DeleteCohabitantAsync(
    Guid id,
    [AsParameters] UserServices services)
    {
        var command = new DeleteCohabitantCommand(id);
        var result = await services.Mediator.Send(command);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok();
    }

    /// <summary>
    /// Crea una nueva residencia.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de creación de residencia.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El ID de la residencia creada.</returns>
    public static async Task<Results<Ok<ResidenceViewModel>, BadRequest<string>, ProblemHttpResult>> CreateResidenceAsync(
      [FromHeader(Name = "x-requestid")] Guid requestId,
      CreateResidenceRequest request,
      [AsParameters] UserServices services)
    {
        var createResidenceCommand = new CreateResidenceCommand(request);
        var requestAddResidence = new IdentifiedCommand<CreateResidenceCommand, Result<ResidenceViewModel>>(createResidenceCommand, requestId);

        var residenceResult = await services.Mediator.Send(requestAddResidence);

        if (!residenceResult.Success)
        {
            return TypedResults.Problem(detail: residenceResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(residenceResult.Data);
    }

    /// <summary>
    /// Actualiza una residencia existente.
    /// </summary>
    /// <param name="request">Solicitud de actualización de residencia.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>La residencia actualizada.</returns>
    public static async Task<Results<Ok<ResidenceViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateResidenceAsync(
      UpdateResidenceRequest request,
      [AsParameters] UserServices services)
    {
        var updateResidenceCommand = new UpdateResidenceCommand(request);

        var residenceResult = await services.Mediator.Send(updateResidenceCommand);

        if (!residenceResult.Success)
        {
            return TypedResults.Problem(detail: residenceResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(residenceResult.Data);

    }

    /// <summary>
    /// Actualiza la residencia actual de un contrato de servicio.
    /// </summary>
    /// <param name="request">Solicitud de actualización de residencia actual.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El contrato de servicio actualizado.</returns>
    public static async Task<Results<Ok<FullServiceContractViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateCurrentResidenceAsync(
      UpdateCurrentResidenceRequest request,
      [AsParameters] UserServices services)
    {
        var updateCurrentResidenceCommand = new UpdateCurrentResidenceCommand(request);
        var result = await services.Mediator.Send(updateCurrentResidenceCommand);
        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }
        return TypedResults.Ok(result.Data);
    }

    /// <summary>
    /// Elimina una residencia por su ID.
    /// </summary>
    /// <param name="id">ID de la residencia.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>Resultado de la eliminación.</returns>
    public static async Task<Results<Ok, NotFound<string>, ProblemHttpResult>> DeleteResidenceAsync(
      Guid id,
      [AsParameters] UserServices services)
    {
        var deleteResidenceCommand = new DeleteResidenceCommand(id);
        var result = await services.Mediator.Send(deleteResidenceCommand);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok();
    }

    /// <summary>
    /// Actualiza el estado actual de un contrato de servicio.
    /// </summary>
    /// <param name="request">Solicitud de actualización de estado de contrato de servicio.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El contrato de servicio actualizado.</returns>
    public static async Task<Results<Ok<FullServiceContractViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateServiceContractStatusAsync(
      UpdateServiceContractStatusRequest request,
      [AsParameters] UserServices services)
    {
        var updateServiceContractStatusCommand = new UpdateServiceContractStatusCommand(request);
        var result = await services.Mediator.Send(updateServiceContractStatusCommand);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok(result.Data);
    }

    public static async Task<Results<Ok<Guid>, BadRequest<string>, ProblemHttpResult>> AddCentralUnitAsync(
    AddCentralUnitRequest request,
    [AsParameters] UserServices services)
    {
        var command = new AddCentralUnitCommand(request);
        var result = await services.Mediator.Send(command);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok(result.Data);
    }

    public static async Task<Results<Ok<Guid>, BadRequest<string>, ProblemHttpResult>> AddPeripheralAsync(
        AddPeripheralRequest request,
        [AsParameters] UserServices services)
    {
        var command = new AddPeripheralCommand(request);
        var result = await services.Mediator.Send(command);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }


        return TypedResults.Ok(result.Data);

    }

    public static async Task<Results<Ok, NotFound<string>, ProblemHttpResult>> RemoveCentralUnitAsync(
        Guid Id,
        [AsParameters] UserServices services)
    {
        var command = new RemoveCentralUnitCommand(Id);
        var result = await services.Mediator.Send(command);
        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, NotFound<string>, ProblemHttpResult>> RemovePeripheralAsync(
        Guid Id,
        [AsParameters] UserServices services)
    {
        var command = new RemovePeripheralCommand(Id);
        var result = await services.Mediator.Send(command);
        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }
        return TypedResults.Ok();
    }

    /// <summary>
    /// Crea una nueva llave.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de creación de llave.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El ID de la llave creada.</returns>
    public static async Task<Results<Ok<Guid>, BadRequest<string>, ProblemHttpResult>> CreateKeyAsync(
      [FromHeader(Name = "x-requestid")] Guid requestId,
      CreateKeyRequest request,
      [AsParameters] UserServices services)
    {
        var createKeyCommand = new CreateKeyCommand(request);
        var requestCreateKey = new IdentifiedCommand<CreateKeyCommand, Result<Guid>>(createKeyCommand, requestId);

        var keyResult = await services.Mediator.Send(requestCreateKey);

        if (!keyResult.Success)
        {
            return TypedResults.Problem(detail: keyResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(keyResult.Data);
    }

    /// <summary>
    /// Actualiza una llave existente.
    /// </summary>
    /// <param name="request">Solicitud de actualización de llave.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>La llave actualizada.</returns>
    public static async Task<Results<Ok<KeyViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateKeyAsync(
      UpdateKeyRequest request,
      [AsParameters] UserServices services)
    {
        var updateKeyCommand = new UpdateKeyCommand(request);

        var keyResult = await services.Mediator.Send(updateKeyCommand);

        if (!keyResult.Success)
        {
            return TypedResults.Problem(detail: keyResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(keyResult.Data);
    }

    /// <summary>
    /// Elimina una llave por su ID.
    /// </summary>
    /// <param name="keyId">ID de la llave.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>Resultado de la eliminación.</returns>
    public static async Task<Results<Ok, NotFound<string>, ProblemHttpResult>> DeleteKeyAsync(
      Guid id,
      [AsParameters] UserServices services)
    {
        var deleteKeyCommand = new DeleteKeyCommand(id);
        var result = await services.Mediator.Send(deleteKeyCommand);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok();
    }

    /// <summary>
    /// Añade un beneficiario a un contrato de servicio.
    /// </summary>
    /// <param name="request">Solicitud para añadir un beneficiario.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El ID del beneficiario añadido.</returns>
    public static async Task<Results<Ok<Guid>, BadRequest<string>, ProblemHttpResult>> AddBeneficiaryAsync(
      AddBeneficiaryRequest request,
      [AsParameters] UserServices services)
    {
        var command = new AddBeneficiaryCommand(request);
        var result = await services.Mediator.Send(command);
        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }
        return TypedResults.Ok(result.Data);
    }

    /// <summary>
    /// Elimina un beneficiario de un contrato de servicio.
    /// </summary>
    /// <param name="id">ID del beneficiario a eliminar.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>Resultado de la eliminación.</returns>
    public static async Task<Results<Ok, NotFound<string>, ProblemHttpResult>> RemoveBeneficiaryAsync(
        Guid id,
        [AsParameters] UserServices services)
    {
        var command = new RemoveBeneficiaryCommand(id);
        var result = await services.Mediator.Send(command);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok();
    }

    /// <summary>
    /// Actualiza una unidad central existente.
    /// </summary>
    /// <param name="request">Solicitud de actualización de unidad central.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>La unidad central actualizada.</returns>
    public static async Task<Results<Ok<CentralUnitViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateCentralUnitAsync(
        UpdateCentralUnitRequest request,
        [AsParameters] UserServices services)
    {
        var command = new UpdateCentralUnitCommand(request);
        var result = await services.Mediator.Send(command);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok(result.Data);
    }

    /// <summary>
    /// Actualiza un periférico existente.
    /// </summary>
    /// <param name="request">Solicitud de actualización de periférico.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El periférico actualizado.</returns>
    public static async Task<Results<Ok<PeripheralViewModel>, BadRequest<string>, ProblemHttpResult>> UpdatePeripheralAsync(
        UpdatePeripheralRequest request,
        [AsParameters] UserServices services)
    {
        var command = new UpdatePeripheralCommand(request);
        var result = await services.Mediator.Send(command);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok(result.Data);
    }

    #endregion
}