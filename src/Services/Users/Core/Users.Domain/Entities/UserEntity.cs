namespace Users.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }
    }
}
