using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Categories.BLL.Entities;

namespace Categories.BLL.Contracts.Repositories
{
    public interface ICategoryRepository
    {
        public void Create(CategoryEntity entity);
        public void Update(CategoryEntity entity);
        public void Delete(CategoryEntity entity);
    }
}
