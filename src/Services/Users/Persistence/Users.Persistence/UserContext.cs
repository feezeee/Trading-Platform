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

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().SetUp();
            modelBuilder.Entity<RoleEntity>().SetUp();
            modelBuilder.Entity<RefreshTokenEntity>().SetUp();
        }
    }
}
