using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Persistence.Configurations.ModelBuilderConfig
{
    internal static partial class Configuration
    {
        internal static void SetUp(this EntityTypeBuilder<RoleEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(t => t.Id);
            entityTypeBuilder
                .Property(t => t.Id)
                .ValueGeneratedNever();

            entityTypeBuilder
                .Property(t => t.Name)
                .IsRequired();

            entityTypeBuilder
                .HasIndex(t => t.Name)
                .IsUnique(true);

            entityTypeBuilder
                .HasMany(t => t.Users)
                .WithMany(t => t.Roles);
        }
    }
}
