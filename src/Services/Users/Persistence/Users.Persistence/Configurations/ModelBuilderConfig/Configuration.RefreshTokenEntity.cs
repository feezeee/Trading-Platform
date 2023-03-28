using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Persistence.Configurations.ModelBuilderConfig
{
    internal static partial class Configuration
    {
        internal static void SetUp(this EntityTypeBuilder<RefreshTokenEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(t => t.Id);
            entityTypeBuilder
                .Property(t => t.Id)
                .ValueGeneratedNever();

            entityTypeBuilder
                .Property(t => t.RefreshToken)
                .IsRequired();
            
            entityTypeBuilder
                .Property(t => t.Created)
                .IsRequired();

            entityTypeBuilder
                .Property(t => t.Expires)
                .IsRequired();

            entityTypeBuilder
                .Property(t => t.UserId)
                .IsRequired();

            entityTypeBuilder
                .Ignore(t => t.IsActive);
            entityTypeBuilder
                .Ignore(t => t.IsExpired);

            entityTypeBuilder
                .HasOne(t => t.User)
                .WithMany(t => t.RefreshTokens)
                .HasForeignKey(t => t.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
