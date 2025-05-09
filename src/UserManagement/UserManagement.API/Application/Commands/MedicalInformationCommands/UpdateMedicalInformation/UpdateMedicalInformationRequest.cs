using UserManagement.API.Application.Commands.MedicalInformationCommands.CreateMedicalInformation;

namespace UserManagement.API.Application.Commands.MedicalInformationCommands.UpdateMedicalInformation;

public class UpdateMedicalInformationRequest : CreateMedicalInformationRequest
{
    public Guid Id { get; set; }

}