using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Mappings;

public class ServiceMap : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Service");

        builder.HasKey(service => service.Id);

        builder.Property(service => service.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn()
            .HasColumnName("Id");

        builder.Property(service => service.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();
        
        builder.Property(service => service.Price)
            .HasColumnType("MONEY")
            .HasColumnName("Price")
            .IsRequired();
        
        builder.Property(service => service.ImageUrl)
            .HasColumnName("Image")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(service => service.Id, "IX_Service_Id")
            .IsUnique();
        
        builder.HasIndex(service => service.Name, "IX_Service_Name")
            .IsUnique();
    }
}