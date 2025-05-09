namespace UserManagement.API.Application.IntegrationEvents.Events;

public record ProfessionalUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; }
    public string Name { get; }
    public string Surname1 { get; }
    public string? Surname2 { get; }

    public ProfessionalUpdatedIntegrationEvent(Guid id, string name, string surname1, string surname2)
    {
        Id = id;
        Name = name;
        Surname1 = surname1;
        Surname2 = surname2;
    }
}
