namespace Users.Domain.Entities
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public string? ReplacedByToken { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => !IsExpired;

        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }

    }
}
