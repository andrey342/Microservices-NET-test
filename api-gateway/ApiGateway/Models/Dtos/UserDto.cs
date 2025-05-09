namespace ApiGateway.Models.Dtos
{
    public class UserDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Surname2 { get; set; } = null;
        public string? Appellative { get; set; } = null;
        public string? Email { get; set; } = null;
        public Guid SexId { get; set; }
        public DateTime Birthdate { get; set; }
        public bool? CongratulateOnBirthDate { get; set; } = null;
    }
}
