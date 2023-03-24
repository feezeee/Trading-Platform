using Users.Domain.Entities;

namespace Users.Domain.Contracts.Findres
{
    public interface IUserFinder
    {
        public Task<UserEntity?> GetUserAsync(string email, string password, CancellationToken token = default);
    }
}
