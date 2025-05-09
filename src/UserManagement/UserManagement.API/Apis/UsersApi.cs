using Microsoft.AspNetCore.Http.HttpResults;
using UserManagement.API.Application.Commands.AllergyImpactCommands.CreateAllergyImpact;
using UserManagement.API.Application.Commands.AllergyImpactCommands.DeleteAllergyImpact;
using UserManagement.API.Application.Commands.AllergyImpactCommands.UpdateAllergyImpact;
using UserManagement.API.Application.Commands.HealthCoverageCommands.CreateHealthCoverage;
using UserManagement.API.Application.Commands.HealthCoverageCommands.DeleteHealthCoverage;
using UserManagement.API.Application.Commands.HealthCoverageCommands.UpdateHealthCoverage;
using UserManagement.API.Application.Commands.IdentifiedCommands;
using UserManagement.API.Application.Commands.MedicalConditionCommands.AddMedicalCondition;
using UserManagement.API.Application.Commands.MedicalConditionCommands.RemoveMedicalCondition;
using UserManagement.API.Application.Commands.MedicalConditionCommands.UpdateMedicalCondition;
using UserManagement.API.Application.Commands.MedicalInformationCommands.CreateMedicalInformation;
using UserManagement.API.Application.Commands.MedicalInformationCommands.DeleteMedicalInformation;
using UserManagement.API.Application.Commands.MedicalInformationCommands.UpdateMedicalInformation;
using UserManagement.API.Application.Commands.MedicationCommands.CreateMedication;
using UserManagement.API.Application.Commands.MedicationCommands.DeleteMedication;
using UserManagement.API.Application.Commands.MedicationCommands.UpdateMedication;
using UserManagement.API.Application.Commands.PersonalResourceCommands.CreatePersonalResource;
using UserManagement.API.Application.Commands.PersonalResourceCommands.DeletePersonalResource;
using UserManagement.API.Application.Commands.PersonalResourceCommands.UpdatePersonalResource;
using UserManagement.API.Application.Commands.UserCommands.CreateUser;
using UserManagement.API.Application.Commands.UserCommands.DeleteUser;
using UserManagement.API.Application.Commands.UserCommands.UpdateUser;
using UserManagement.API.Application.Commands.WorkCenterResourceCommands.CreateWorkCenterResource;
using UserManagement.API.Application.Commands.WorkCenterResourceCommands.DeleteWorkCenterResource;
using UserManagement.API.Application.Commands.WorkCenterResourceCommands.UpdateWorkCenterResource;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

public static class UsersApi
{
    public static RouteGroupBuilder MapUsersApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("user").HasApiVersion(1.0);

        #region Apis principales

        api.MapGet("/getAll", GetAll);
        api.MapGet("/getById/{id:Guid}", GetUserAsync);
        api.MapGet("/getByIdentificationNumberAndWorkcenterId", GetUserByIdentificationNumberAsync);
        api.MapGet("/getPsycologicalScale", GetPsycologicalScale);
        api.MapGet("/getPhysicalScale", GetPhysicalScale);
        api.MapGet("/getSocialScale", GetSocialScale);
        //api.MapPost("/create", CreateUserAsync); // El user se crea auto en el contrato
        api.MapPut("/update", UpdateUserAsync);
        //api.MapDelete("/delete/{id:Guid}", DeleteUserAsync); // El user se borra auto con el contrato
        
        api.MapPost("/createMedicalInformation", CreateMedicalInformationAsync);
        api.MapPut("/updateMedicalInformation", UpdateMedicalInformationAsync);
        api.MapDelete("/deleteMedicalInformation", DeleteMedicalInformationAsync);
        api.MapPost("/addMedicalCondition", AddMedicalConditionAsync);
        api.MapPut("/updateMedicalCondition", UpdateMedicalConditionAsync);
        api.MapDelete("/removeMedicalCondition", RemoveMedicalConditionAsync);
        api.MapPost("/addAllergyImpact", AddAllergyImpactAsync);
        api.MapPut("/updateAllergyImpact", UpdateAllergyImpactAsync);
        api.MapDelete("/removeAllergyImpact", RemoveAllergyImpactAsync);
        api.MapPost("/addHealthCoverage", AddHealthCoverageAsync);
        api.MapPut("/updateHealthCoverage", UpdateHealthCoverageAsync);
        api.MapDelete("/removeHealthCoverage", RemoveHealthCoverageAsync);
        api.MapPost("/addMedication", AddMedicationAsync);
        api.MapPut("/updateMedication", UpdateMedicationAsync);
        api.MapDelete("/removeMedication", RemoveMedicationAsync);

        api.MapGet("/getMedicalInfoByUserId/{userId:Guid}", GetMedicalInfoByUserIdAsync);
        api.MapGet("/getMedicaConditionByMedicalInformationId/{medicalInformationId:Guid}", GetMedicalConditionByMedicalInfoIdAsync);
        api.MapGet("/getAllergyImpactByMedicalInformationId/{medicalInformationId:Guid}", GetAllergyImpactByMedicalInfoIdAsync);
        api.MapGet("/getHealthCoverageByMedicalInformationId/{medicalInformationId:Guid}", GetHealthCoverageByMedicalInfoIdAsync);
        api.MapGet("/getMedicationByMedicalInformationId/{medicalInformationId:Guid}", GetMedicationByMedicalInfoIdAsync);

        // WorkCenterResource
        api.MapGet("/getWorkCenterResourcesByUserId/{userId:Guid}", GetResourcesByUserIdAsync);
        api.MapPost("/addWorkCenterResource", AddWorkCenterResourceAsync);
        api.MapPut("/updateWorkCenterResource", UpdateWorkCenterResourceAsync);
        api.MapDelete("/removeWorkCenterResource/{id:Guid}", RemoveWorkCenterResourceAsync);
        // getPersonalResources
        api.MapGet("/getPersonalResourcesByUserId/{userId:Guid}", GetPersonalResourcesByUserIdAsync);
        api.MapPost("/addPersonalResource", AddPersonalResourceAsync);
        api.MapPut("/updatePersonalResource", UpdatePersonalResourceAsync);
        api.MapDelete("/removePersonalResource/{id:Guid}", RemovePersonalResourceAsync);
        // User History
        api.MapGet("/getHistoryByUserId/{userId:Guid}", GetHistoryByUserIdAsync);
        #endregion

        #region Maestros

        // Lista de maestros con su tipo de Entidad y su ViewModel
        var masters = new List<(string Route, Type EntityType, Type ViewModelType)>
        {
            ("sexes", typeof(Sex), typeof(SexViewModel)),
            ("languages", typeof(Language), typeof(LanguageViewModel)),
            ("civilstatuses", typeof(CivilStatus), typeof(CivilStatusViewModel)),
            ("educations", typeof(Education), typeof(EducationViewModel)),
            ("dependencies", typeof(DependencyDegree), typeof(DependencyDegreeViewModel)),
            ("identificationtypes", typeof(IdentificationType), typeof(IdentificationTypeViewModel)),
            ("animals", typeof(Animal), typeof(AnimalViewModel)),

            ("diseases", typeof(Disease), typeof(DiseaseViewModel)),
            ("medicalConditionStatuses", typeof(MedicalConditionStatus), typeof(MedicalConditionStatusViewModel)),
            ("medicines", typeof(Medicine), typeof(MedicineViewModel)),
            ("allergies", typeof(Allergy), typeof(AllergyViewModel)),
            ("allergySeverities", typeof(AllergySeverity), typeof(AllergySeverityViewModel)),
        };

        foreach (var (route, entityType, viewModelType) in masters)
        {
            // Utilizamos un método genérico para definir los endpoints con el tipo correcto
            typeof(UsersApi)
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
        api.MapGet($"/{route}", async ([FromServices] IUserQueries queries) =>
            TypedResults.Ok(await queries.GetAllMastersAsync<TMaster, TViewModel>())
        )
        .Produces<IEnumerable<TViewModel>>(StatusCodes.Status200OK) // Define la respuesta en Swagger
        .WithName($"Get{typeof(TViewModel).Name}") // Nombre único en Swagger
        .WithTags("Masters"); // Categoría en Swagger
    }

    #endregion

    #region Queries

    /// <summary>
    /// Obtiene una lista de todos los usuarios en formato básico.
    /// </summary>
    /// <param name="queries">Consultas de usuario.</param>
    /// <returns>Una lista de usuarios en formato básico.</returns>
    public static async Task<Ok<IEnumerable<BasicUserViewModel>>> GetAll([FromServices] IUserQueries queries)
    {
        var users = await queries.GetAllUsersAsync();
        return TypedResults.Ok(users);
    }

    /// <summary>
    /// Obtiene los detalles completos de un usuario por su ID.
    /// </summary>
    /// <param name="id">ID del usuario.</param>
    /// <param name="queries">Consultas de usuario.</param>
    /// <returns>El usuario correspondiente al ID proporcionado.</returns>
    public static async Task<Results<Ok<FullUserViewModel>, NotFound<string>>> GetUserAsync(Guid id, [FromServices] IUserQueries queries)
    {
        try
        {
            var user = await queries.GetUserAsync(id);
            return TypedResults.Ok(user);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene los detalles completos de un usuario por su número de identificación y ID del centro de trabajo.
    /// </summary>
    /// <param name="identificationNumber">Número de identificación del usuario.</param>
    /// <param name="workcenterId">ID del centro de trabajo.</param>
    /// <param name="queries">Consultas de usuario.</param>
    /// <returns>El usuario correspondiente al número de identificación y ID del centro de trabajo proporcionados.</returns>
    public static async Task<Results<Ok<FullUserViewModel>, ProblemHttpResult>> GetUserByIdentificationNumberAsync(
        string identificationNumber,
        Guid workcenterId,
        [FromServices] IUserQueries queries
        )
    {
        try
        {
            var user = await queries.GetUserByIdentificationNumberAsync(identificationNumber, workcenterId);
            return TypedResults.Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            // Respuesta 404 usuario no encontrado
            return TypedResults.Problem(detail: ex.Message, statusCode: 404);
        }
        catch (InvalidOperationException ex)
        {
            // Respuesta 409 usuario duplicado en el mismo workcenter
            return TypedResults.Problem(detail: ex.Message, statusCode: 409);
        }
        catch (Exception ex)
        {
            // Cualquier otro error => 500
            return TypedResults.Problem(detail: ex.Message, statusCode: 500);
        }
    }

    /// <summary>
    /// Obtiene la escala psicológica.
    /// </summary>
    /// <returns>Una lista de valores de la escala psicológica.</returns>
    public static async Task<Ok<IEnumerable<string>>> GetPsycologicalScale()
    {
        var values = PsychologicalScale.GetPsychologicalScale();
        return TypedResults.Ok(values);
    }

    /// <summary>
    /// Obtiene la escala física.
    /// </summary>
    /// <returns>Una lista de valores de la escala física.</returns>
    public static async Task<Ok<IEnumerable<string>>> GetPhysicalScale()
    {
        var values = PhysicalScale.GetPhysicalScale();
        return TypedResults.Ok(values);
    }

    /// <summary>
    /// Obtiene la escala social.
    /// </summary>
    /// <returns>Una lista de valores de la escala social.</returns>
    public static async Task<Ok<IEnumerable<string>>> GetSocialScale()
    {
        var values = SocialScale.GetSocialScale();
        return TypedResults.Ok(values);
    }

    public static async Task<Results<Ok<MedicalInformationViewModel>, NotFound<string>>> GetMedicalInfoByUserIdAsync(Guid userId, [FromServices] IUserQueries queries)
    {
        try
        {
            var user = await queries.GetMedicalInfoByUserIdAsync(userId);
            return TypedResults.Ok(user);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene las condiciones médicas asociadas a una información médica específica.
    /// </summary>
    /// <param name="medicalInfoId">ID de la información médica.</param>
    /// <param name="queries">Consultas de usuario.</param>
    /// <returns>Una lista de condiciones médicas asociadas a la información médica.</returns>
    public static async Task<Results<Ok<IEnumerable<MedicalConditionViewModel>>, NotFound<string>>> GetMedicalConditionByMedicalInfoIdAsync(
        Guid medicalInformationId,
        [FromServices] IUserQueries queries)
    {
        try
        {
            var medicalConditions = await queries.GetMedicalConditionByMedicalInfoIdAsync(medicalInformationId);
            return TypedResults.Ok(medicalConditions);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene los impactos de alergia asociados a una información médica específica.
    /// </summary>
    /// <param name="medicalInformationId">ID de la información médica.</param>
    /// <param name="queries">Consultas de usuario.</param>
    /// <returns>Una lista de impactos de alergia asociados a la información médica.</returns>
    public static async Task<Results<Ok<IEnumerable<AllergyImpactViewModel>>, NotFound<string>>> GetAllergyImpactByMedicalInfoIdAsync(
        Guid medicalInformationId,
        [FromServices] IUserQueries queries)
    {
        try
        {
            var allergyImpacts = await queries.GetAllergyImpactByMedicalInfoIdAsync(medicalInformationId);
            return TypedResults.Ok(allergyImpacts);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene las coberturas de salud asociadas a una información médica específica.
    /// </summary>
    /// <param name="medicalInformationId">ID de la información médica.</param>
    /// <param name="queries">Consultas de usuario.</param>
    /// <returns>Una lista de coberturas de salud asociadas a la información médica.</returns>
    public static async Task<Results<Ok<IEnumerable<HealthCoverageViewModel>>, NotFound<string>>> GetHealthCoverageByMedicalInfoIdAsync(
        Guid medicalInformationId,
        [FromServices] IUserQueries queries)
    {
        try
        {
            var healthCoverages = await queries.GetHealthCoverageByMedicalInfoIdAsync(medicalInformationId);
            return TypedResults.Ok(healthCoverages);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene los medicamentos asociados a una información médica específica.
    /// </summary>
    /// <param name="medicalInformationId">ID de la información médica.</param>
    /// <param name="queries">Consultas de usuario.</param>
    /// <returns>Una lista de medicamentos asociados a la información médica.</returns>
    public static async Task<Results<Ok<IEnumerable<MedicationViewModel>>, NotFound<string>>> GetMedicationByMedicalInfoIdAsync(
        Guid medicalInformationId,
        [FromServices] IUserQueries queries)
    {
        try
        {
            var medications = await queries.GetMedicationByMedicalInfoIdAsync(medicalInformationId);
            return TypedResults.Ok(medications);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    public static async Task<Results<Ok<IEnumerable<WorkCenterResourceViewModel>>, NotFound<string>>> GetResourcesByUserIdAsync(
    Guid userId,
    [FromServices] IUserQueries queries)
    {
        try
        {
            var result = await queries.GetResourcesByUserIdAsync(userId);
            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    public static async Task<Ok<IEnumerable<PersonalResourceViewModel>>> GetPersonalResourcesByUserIdAsync(
    Guid userId,
    [FromServices] IUserQueries queries)
    {
        var result = await queries.GetPersonalResourcesByUserIdAsync(userId);
        return TypedResults.Ok(result);
    }

    public static async Task<Results<Ok<IEnumerable<UserHistoryViewModel>>, NotFound<string>>> GetHistoryByUserIdAsync(
    Guid userId,
    [FromServices] IUserQueries queries)
    {
        try
        {
            var history = await queries.GetHistoryByUserIdAsync(userId);
            return TypedResults.Ok(history);
        }
        catch (KeyNotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    #endregion

    #region Commands

    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de creación de usuario.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El ID del usuario creado.</returns>
    public static async Task<Results<Ok<Guid>, BadRequest<string>, ProblemHttpResult>> CreateUserAsync(
      [FromHeader(Name = "x-requestid")] Guid requestId,
      CreateUserRequest request,
      [AsParameters] UserServices services)
    {
        var createUserCommand = new CreateUserCommand(request);
        var requestCreateOrder = new IdentifiedCommand<CreateUserCommand, Result<Guid>>(createUserCommand, requestId);

        var userResult = await services.Mediator.Send(requestCreateOrder);

        if (!userResult.Success)
        {
            return TypedResults.Problem(detail: userResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(userResult.Data);
    }

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    /// <param name="request">Solicitud de actualización de usuario.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El usuario actualizado.</returns>
    public static async Task<Results<Ok<BasicUserViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateUserAsync(
    UpdateUserRequest request,
    [AsParameters] UserServices services)
    {
        var updateUserCommand = new UpdateUserCommand(request);
        var result = await services.Mediator.Send(updateUserCommand);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok(result.Data);
    }

    /// <summary>
    /// Elimina un usuario por su ID.
    /// </summary>
    /// <param name="id">ID del usuario.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>Resultado de la eliminación.</returns>
    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> DeleteUserAsync(
        [FromRoute] Guid id,
        [AsParameters] UserServices services
    )
    {
        var command = new DeleteUserCommand { Id = id };
        var result = await services.Mediator.Send(command);

        if (!result.Success)
        {
            return TypedResults.Problem(detail: result.Message, statusCode: 500);
        }

        return TypedResults.Ok();
    }

    /// <summary>
    /// Crea una nueva información médica.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de creación de información médica.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El ID de la información médica creada.</returns>
    public static async Task<Results<Ok<MedicalInformationViewModel>, BadRequest<string>, ProblemHttpResult>> CreateMedicalInformationAsync(
      [FromHeader(Name = "x-requestid")] Guid requestId,
      CreateMedicalInformationRequest request,
      [AsParameters] UserServices services)
    {
        var createMedicalInformationCommand = new CreateMedicalInformationCommand(request);
        var requestCreateMedicalInformation = new IdentifiedCommand<CreateMedicalInformationCommand, Result<MedicalInformationViewModel>>(createMedicalInformationCommand, requestId);

        var medicalInformationResult = await services.Mediator.Send(requestCreateMedicalInformation);

        if (!medicalInformationResult.Success)
        {
            return TypedResults.Problem(detail: medicalInformationResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(medicalInformationResult.Data);
    }

    /// <summary>
    /// Actualiza una información médica existente.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de actualización de información médica.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>La información médica actualizada.</returns>
    public static async Task<Results<Ok<MedicalInformationViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateMedicalInformationAsync(
      UpdateMedicalInformationRequest request,
      [AsParameters] UserServices services)
    {
        var updateMedicalInformationCommand = new UpdateMedicalInformationCommand(request);

        var medicalInformationResult = await services.Mediator.Send(updateMedicalInformationCommand);

        if (!medicalInformationResult.Success)
        {
            return TypedResults.Problem(detail: medicalInformationResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(medicalInformationResult.Data);
    }

    /// <summary>
    /// Elimina una información médica por su ID.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="userId">ID del usuario.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>Resultado de la eliminación.</returns>
    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> DeleteMedicalInformationAsync(
        Guid userId,
        [AsParameters] UserServices services)
    {
        var deleteMedicalInformationCommand = new DeleteMedicalInformationCommand(userId);

        var medicalInformationResult = await services.Mediator.Send(deleteMedicalInformationCommand);

        if (!medicalInformationResult.Success)
        {
            return TypedResults.Problem(detail: medicalInformationResult.Message, statusCode: 500);
        }

        return TypedResults.Ok();
    }

    /// <summary>
    /// Añade una nueva condición médica a la información médica existente.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de adición de condición médica.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El ID de la condición médica añadida.</returns>
    public static async Task<Results<Ok<Guid>, BadRequest<string>, ProblemHttpResult>> AddMedicalConditionAsync(
        [FromHeader(Name = "x-requestid")] Guid requestId,
        CreateMedicalConditionRequest request,
        [AsParameters] UserServices services)
    {
        var addMedicalConditionCommand = new CreateMedicalConditionCommand(request);
        var requestAddMedicalCondition = new IdentifiedCommand<CreateMedicalConditionCommand, Result<Guid>>(addMedicalConditionCommand, requestId);

        var medicalConditionResult = await services.Mediator.Send(requestAddMedicalCondition);

        if (!medicalConditionResult.Success)
        {
            return TypedResults.Problem(detail: medicalConditionResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(medicalConditionResult.Data);
    }

    /// <summary>
    /// Elimina una condición médica existente de la información médica.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="medicalConditionId">ID de la condición médica a eliminar.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>Resultado de la eliminación.</returns>
    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> RemoveMedicalConditionAsync(
        Guid medicalConditionId,
        [AsParameters] UserServices services)
    {
        var removeMedicalConditionCommand = new DeleteMedicalConditionCommand(medicalConditionId);

        var medicalConditionResult = await services.Mediator.Send(removeMedicalConditionCommand);

        if (!medicalConditionResult.Success)
        {
            return TypedResults.Problem(detail: medicalConditionResult.Message, statusCode: 500);
        }

        return TypedResults.Ok();
    }

    /// <summary>
    /// Actualiza una condición médica existente en la información médica.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de actualización de condición médica.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>La condición médica actualizada.</returns>
    public static async Task<Results<Ok<MedicalConditionViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateMedicalConditionAsync(
        UpdateMedicalConditionRequest request,
        [AsParameters] UserServices services)
    {
        var updateMedicalConditionCommand = new UpdateMedicalConditionCommand(request);

        var medicalConditionResult = await services.Mediator.Send(updateMedicalConditionCommand);

        if (!medicalConditionResult.Success)
        {
            return TypedResults.Problem(detail: medicalConditionResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(medicalConditionResult.Data);
    }

    /// <summary>
    /// Añade un nuevo impacto de alergia a la información médica existente.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de adición de impacto de alergia.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El ID del impacto de alergia añadido.</returns>
    public static async Task<Results<Ok<Guid>, BadRequest<string>, ProblemHttpResult>> AddAllergyImpactAsync(
        [FromHeader(Name = "x-requestid")] Guid requestId,
        CreateAllergyImpactRequest request,
        [AsParameters] UserServices services)
    {
        var addAllergyImpactCommand = new CreateAllergyImpactCommand(request);
        var requestAddAllergyImpact = new IdentifiedCommand<CreateAllergyImpactCommand, Result<Guid>>(addAllergyImpactCommand, requestId);

        var allergyImpactResult = await services.Mediator.Send(requestAddAllergyImpact);

        if (!allergyImpactResult.Success)
        {
            return TypedResults.Problem(detail: allergyImpactResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(allergyImpactResult.Data);
    }

    /// <summary>
    /// Actualiza un impacto de alergia existente en la información médica.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de actualización de impacto de alergia.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El impacto de alergia actualizado.</returns>
    public static async Task<Results<Ok<AllergyImpactViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateAllergyImpactAsync(
        UpdateAllergyImpactRequest request,
        [AsParameters] UserServices services)
    {
        var updateAllergyImpactCommand = new UpdateAllergyImpactCommand(request);

        var allergyImpactResult = await services.Mediator.Send(updateAllergyImpactCommand);

        if (!allergyImpactResult.Success)
        {
            return TypedResults.Problem(detail: allergyImpactResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(allergyImpactResult.Data);
    }

    /// <summary>
    /// Elimina un impacto de alergia existente de la información médica.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="allergyImpactId">ID del impacto de alergia a eliminar.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>Resultado de la eliminación.</returns>
    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> RemoveAllergyImpactAsync(
        Guid allergyImpactId,
        [AsParameters] UserServices services)
    {
        var removeAllergyImpactCommand = new DeleteAllergyImpactCommand(allergyImpactId);

        var allergyImpactResult = await services.Mediator.Send(removeAllergyImpactCommand);

        if (!allergyImpactResult.Success)
        {
            return TypedResults.Problem(detail: allergyImpactResult.Message, statusCode: 500);
        }

        return TypedResults.Ok();
    }

    /// <summary>
    /// Añade una nueva cobertura de salud a la información médica existente.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de adición de cobertura de salud.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El ID de la cobertura de salud añadida.</returns>
    public static async Task<Results<Ok<Guid>, BadRequest<string>, ProblemHttpResult>> AddHealthCoverageAsync(
        [FromHeader(Name = "x-requestid")] Guid requestId,
        CreateHealthCoverageRequest request,
        [AsParameters] UserServices services)
    {
        var addHealthCoverageCommand = new CreateHealthCoverageCommand(request);
        var requestAddHealthCoverage = new IdentifiedCommand<CreateHealthCoverageCommand, Result<Guid>>(addHealthCoverageCommand, requestId);

        var healthCoverageResult = await services.Mediator.Send(requestAddHealthCoverage);

        if (!healthCoverageResult.Success)
        {
            return TypedResults.Problem(detail: healthCoverageResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(healthCoverageResult.Data);
    }

    /// <summary>
    /// Actualiza una cobertura de salud existente en la información médica.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de actualización de cobertura de salud.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>La cobertura de salud actualizada.</returns>
    public static async Task<Results<Ok<HealthCoverageViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateHealthCoverageAsync(
        UpdateHealthCoverageRequest request,
        [AsParameters] UserServices services)
    {
        var updateHealthCoverageCommand = new UpdateHealthCoverageCommand(request);

        var healthCoverageResult = await services.Mediator.Send(updateHealthCoverageCommand);

        if (!healthCoverageResult.Success)
        {
            return TypedResults.Problem(detail: healthCoverageResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(healthCoverageResult.Data);
    }

    /// <summary>
    /// Elimina una cobertura de salud existente de la información médica.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="healthCoverageId">ID de la cobertura de salud a eliminar.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>Resultado de la eliminación.</returns>
    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> RemoveHealthCoverageAsync(
        Guid healthCoverageId,
        [AsParameters] UserServices services)
    {
        var removeHealthCoverageCommand = new DeleteHealthCoverageCommand(healthCoverageId);

        var healthCoverageResult = await services.Mediator.Send(removeHealthCoverageCommand);

        if (!healthCoverageResult.Success)
        {
            return TypedResults.Problem(detail: healthCoverageResult.Message, statusCode: 500);
        }

        return TypedResults.Ok();
    }

    /// <summary>
    /// Añade un nuevo medicamento a la información médica existente.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de adición de medicamento.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El ID del medicamento añadido.</returns>
    public static async Task<Results<Ok<Guid>, BadRequest<string>, ProblemHttpResult>> AddMedicationAsync(
        [FromHeader(Name = "x-requestid")] Guid requestId,
        CreateMedicationRequest request,
        [AsParameters] UserServices services)
    {
        var addMedicationCommand = new CreateMedicationCommand(request);
        var requestAddMedication = new IdentifiedCommand<CreateMedicationCommand, Result<Guid>>(addMedicationCommand, requestId);

        var medicationResult = await services.Mediator.Send(requestAddMedication);

        if (!medicationResult.Success)
        {
            return TypedResults.Problem(detail: medicationResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(medicationResult.Data);
    }

    /// <summary>
    /// Actualiza un medicamento existente en la información médica.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="request">Solicitud de actualización de medicamento.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>El medicamento actualizado.</returns>
    public static async Task<Results<Ok<MedicationViewModel>, BadRequest<string>, ProblemHttpResult>> UpdateMedicationAsync(
        UpdateMedicationRequest request,
        [AsParameters] UserServices services)
    {
        var updateMedicationCommand = new UpdateMedicationCommand(request);

        var medicationResult = await services.Mediator.Send(updateMedicationCommand);

        if (!medicationResult.Success)
        {
            return TypedResults.Problem(detail: medicationResult.Message, statusCode: 500);
        }

        return TypedResults.Ok(medicationResult.Data);
    }

    /// <summary>
    /// Elimina un medicamento existente de la información médica.
    /// </summary>
    /// <param name="requestId">ID de la solicitud.</param>
    /// <param name="medicationId">ID del medicamento a eliminar.</param>
    /// <param name="services">Servicios de usuario.</param>
    /// <returns>Resultado de la eliminación.</returns>
    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> RemoveMedicationAsync(
        Guid medicationId,
        [AsParameters] UserServices services)
    {
        var removeMedicationCommand = new DeleteMedicationCommand(medicationId);

        var medicationResult = await services.Mediator.Send(removeMedicationCommand);

        if (!medicationResult.Success)
        {
            return TypedResults.Problem(detail: medicationResult.Message, statusCode: 500);
        }

        return TypedResults.Ok();
    }

    public static async Task<Results<Ok<Guid>, ProblemHttpResult>> AddWorkCenterResourceAsync(
    [FromHeader(Name = "x-requestid")] Guid requestId,
    CreateWorkCenterResourceRequest request,
    [AsParameters] UserServices services)
    {
        var command = new CreateWorkCenterResourceCommand(request);
        var identified = new IdentifiedCommand<CreateWorkCenterResourceCommand, Result<Guid>>(command, requestId);
        var result = await services.Mediator.Send(identified);

        return result.Success
            ? TypedResults.Ok(result.Data)
            : TypedResults.Problem(detail: result.Message, statusCode: 500);
    }

    public static async Task<Results<Ok, ProblemHttpResult>> UpdateWorkCenterResourceAsync(
        UpdateWorkCenterResourceRequest request,
        [AsParameters] UserServices services)
    {
        var command = new UpdateWorkCenterResourceCommand(request);
        var result = await services.Mediator.Send(command);

        return result.Success
            ? TypedResults.Ok()
            : TypedResults.Problem(detail: result.Message, statusCode: 500);
    }

    public static async Task<Results<Ok, ProblemHttpResult>> RemoveWorkCenterResourceAsync(
        Guid id,
        [AsParameters] UserServices services)
    {
        var command = new DeleteWorkCenterResourceCommand(id);
        var result = await services.Mediator.Send(command);

        return result.Success
            ? TypedResults.Ok()
            : TypedResults.Problem(detail: result.Message, statusCode: 500);
    }

    public static async Task<Results<Ok<Guid>, ProblemHttpResult>> AddPersonalResourceAsync(
    [FromHeader(Name = "x-requestid")] Guid requestId,
    CreatePersonalResourceRequest request,
    [AsParameters] UserServices services)
    {
        var command = new CreatePersonalResourceCommand(request);
        var identified = new IdentifiedCommand<CreatePersonalResourceCommand, Result<Guid>>(command, requestId);
        var result = await services.Mediator.Send(identified);

        return result.Success
            ? TypedResults.Ok(result.Data)
            : TypedResults.Problem(detail: result.Message, statusCode: 500);
    }

    public static async Task<Results<Ok, ProblemHttpResult>> UpdatePersonalResourceAsync(
        UpdatePersonalResourceRequest request,
        [AsParameters] UserServices services)
    {
        var command = new UpdatePersonalResourceCommand(request);
        var result = await services.Mediator.Send(command);

        return result.Success
            ? TypedResults.Ok()
            : TypedResults.Problem(detail: result.Message, statusCode: 500);
    }

    public static async Task<Results<Ok, ProblemHttpResult>> RemovePersonalResourceAsync(
        Guid id,
        [AsParameters] UserServices services)
    {
        var command = new DeletePersonalResourceCommand(id);
        var result = await services.Mediator.Send(command);

        return result.Success
            ? TypedResults.Ok()
            : TypedResults.Problem(detail: result.Message, statusCode: 500);
    }


    #endregion
}
