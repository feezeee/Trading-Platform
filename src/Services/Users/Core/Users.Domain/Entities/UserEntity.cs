namespace Users.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

        public ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();

        public ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = new List<RefreshTokenEntity>();

    }
}
