using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Data.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.LastUpdatedAt)
                .HasColumnName(nameof(Role.LastUpdatedAt))
                .HasColumnType("DATETIME")
                .IsRequired(false);
        }
    }
}
