using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Infrastructure.Mappings;

public class ServiceMap : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Service");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn()
            .IsRequired();

        builder.HasOne(s => s.Category)
            .WithMany(c => c.Services)
            .HasConstraintName("FK_Service_Category_CategoryId")
            .HasForeignKey(s => s.CategoryId)
            .IsRequired();

        builder.Property(s => s.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(s => s.ImageUrl)
            .HasColumnName("ImageUrl")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(s => s.DurationInMinutes)
            .HasColumnName("DurationInMinutes")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(s => s.Price)
            .HasColumnName("Price")
            .HasColumnType("DECIMAL(10,2)")
            .IsRequired();

        builder.Property(s => s.Status)
            .HasColumnName("Status")
            .HasConversion(
                eas => eas.ToString(),
                eas => (EActivingStatus)Enum.Parse(typeof(EActivingStatus), eas)
            )
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(s => s.LastUpdatedAt)
            .HasColumnName("LastUpdatedAt")
            .HasColumnType("DATETIME")
            .IsRequired(false);

        builder.HasIndex(s => s.Id, "IX_Service_Id");
        builder.HasIndex(s => s.Name, "IX_Service_Name");
        builder.HasIndex(s => s.CategoryId, "IX_Service_Category_CategoryId");
    }
}