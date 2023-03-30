using Users.Domain.Entities;

namespace Users.Application.Utils.TokenGenerator
{
    public interface ITokenGenerator
    {
        public string GenerateJwtToken(UserEntity user);
        public string GenerateRefreshToken();
    }
}
