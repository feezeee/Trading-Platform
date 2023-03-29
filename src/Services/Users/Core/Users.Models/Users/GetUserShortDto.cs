namespace Users.Models.Users
{
    public class GetUserShortDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }
    }
}
