using Users.Models.RefreshTokens;
using Users.Models.Roles;

namespace Users.Models.Users
{
    public class GetUserFullDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

        public ICollection<GetRolesDto> Roles { get; set; } = new List<GetRolesDto>();
        public ICollection<GetRefreshTokenDto> RefreshTokens { get; set; } = new List<GetRefreshTokenDto>();

    }
}
