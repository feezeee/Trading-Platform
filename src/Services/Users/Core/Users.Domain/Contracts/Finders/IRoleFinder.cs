﻿using Users.Domain.Entities;

namespace Users.Domain.Contracts.Finders
{
    public interface IRoleFinder
    {
        public Task<List<RoleEntity>> GetAllAsync(CancellationToken token = default);
        public Task<List<RoleEntity>> GetAllPaginationAsync(int pageNumber, int pageSize, CancellationToken token = default);

        public Task<RoleEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
        public Task<RoleEntity?> GetByNameAsync(string name, CancellationToken token = default);
        public Task<bool> HasAnyByNameAsync(string name, CancellationToken token = default);
        public Task<bool> HasAnyByIdAsync(Guid id, CancellationToken token = default);
    }
}
