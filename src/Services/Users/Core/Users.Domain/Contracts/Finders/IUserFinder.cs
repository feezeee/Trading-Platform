﻿using Users.Domain.Entities;

namespace Users.Domain.Contracts.Finders
{
    public interface IUserFinder
    {
        public Task<UserEntity?> GetByNicknameAndPasswordAsync(string nickname, string password, CancellationToken token = default);
        public Task<List<UserEntity>> GetAllAsync(CancellationToken token = default);
    }
}
