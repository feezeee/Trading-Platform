using Categories.BLL.Entities;
using Categories.DAL.Configurations.ModelBuilderConfig;
using Microsoft.EntityFrameworkCore;

namespace Categories.DAL.Context
{
    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CategoryEntity> CategoryEntities { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>().SetUp();
        }
    }
}
