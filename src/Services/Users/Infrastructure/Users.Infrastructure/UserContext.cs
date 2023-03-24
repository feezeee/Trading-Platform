using Microsoft.EntityFrameworkCore;
using Users.Domain.Entities;
using Users.Infrastructure.Configurations.ModelBuilderConfig;

namespace Users.Infrastructure
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
