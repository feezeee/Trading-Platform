﻿namespace Users.Domain.Contracts.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
