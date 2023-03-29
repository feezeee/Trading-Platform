using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Persistence.Configurations.ModelBuilderConfig
{
    internal static partial class Configuration
    {
        internal static void SetUp(this EntityTypeBuilder<UserEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(t => t.Id);
            entityTypeBuilder
                .Property(t => t.Id)
                .ValueGeneratedNever();

            entityTypeBuilder
                .Property(t => t.FirstName)
                .IsRequired();

            entityTypeBuilder
                .Property(t => t.LastName)
                .IsRequired();

            entityTypeBuilder
                .Property(t => t.Nickname)
                .IsRequired();

            entityTypeBuilder
                .HasIndex(t => t.Nickname)
                .IsUnique();



            entityTypeBuilder
                .Property(t => t.Password)
                .IsRequired();


            entityTypeBuilder
                .Property(t => t.RegistrationDate)
                .HasDefaultValueSql("getutcdate()")
                .IsRequired();

            entityTypeBuilder
                .HasMany(t => t.Roles)
                .WithMany(t => t.Users);
        }
    }
}
