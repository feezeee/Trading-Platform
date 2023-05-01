using Categories.BLL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Categories.DAL.Configurations.ModelBuilderConfig
{
    internal static partial class Configuration
    {
        internal static void SetUp(this EntityTypeBuilder<CategoryEntity> entityTypeBuilder)
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
                .IsUnique();
        }
    }
}
