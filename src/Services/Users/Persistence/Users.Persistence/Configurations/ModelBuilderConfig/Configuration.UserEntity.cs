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
                .Property(t => t.NickName)
                .IsRequired();

            entityTypeBuilder
                .HasIndex(t => t.NickName)
                .IsUnique();

            entityTypeBuilder
                .Property(t => t.Email)
                .IsRequired();

            entityTypeBuilder
                .HasIndex(t => t.Email)
                .IsUnique();

            entityTypeBuilder
                .Property(t => t.Password)
                .IsRequired();

            entityTypeBuilder
                .Property(t => t.PhoneNumber)
                .IsRequired(false);


            // Todo add default value sql 'utcnow'
            entityTypeBuilder
                .Property(t => t.RegistrationDate)
                .IsRequired();
        }
    }
}
