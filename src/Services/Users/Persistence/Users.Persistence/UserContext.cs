using Microsoft.EntityFrameworkCore;
using Users.Domain.Entities;
using Users.Persistence.Configurations.ModelBuilderConfig;

namespace Users.Persistence
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().SetUp();
        }
    }
}
