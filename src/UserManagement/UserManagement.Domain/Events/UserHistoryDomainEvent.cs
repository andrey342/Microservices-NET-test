using MediatR;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Events
{
    public class UserHistoryDomainEvent : IDomainEvent
    {
        public string Type { get; }
        public string Action { get; }
        public string Description { get; }
        public Guid UserId { get; }
        public Guid? ServiceContractId { get; }
        public DateTime OccurredOn { get; }

        public UserHistoryDomainEvent(string type, string action, string description, Guid userId, Guid? serviceContractId)
        {
            Type = type;
            Action = action;
            Description = description;
            UserId = userId;
            ServiceContractId = serviceContractId;
            OccurredOn = DateTime.UtcNow;
        }
    }

    public static class UserHistoryConstants
    {
        public static class Types
        {
            public const string Contract = "Contrato";
            public const string Beneficiary = "Beneficiario";
            public const string Residence = "Residencia";
            public const string Key = "Llave";
            public const string Device = "Dispositivo";
        }

        public static class Actions
        {
            public const string New = "Nueva";
            public const string Update = "Actualización";
            public const string Delete = "Eliminación";
        }
    }
}