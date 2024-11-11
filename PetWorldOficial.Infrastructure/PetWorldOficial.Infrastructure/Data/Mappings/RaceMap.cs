// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using PetWorldOficial.Domain.Entities;
//
// namespace PetWorldOficial.Infrastructure.Data.Mappings;
//
// public class RaceMap : IEntityTypeConfiguration<Race>
// {
//     public void Configure(EntityTypeBuilder<Race> builder)
//     {
//         builder.ToTable("Race");
//
//         builder.HasKey(r => r.Id);
//
//         builder.Property(r => r.Id)
//             .ValueGeneratedOnAdd()
//             .UseIdentityColumn()
//             .IsRequired();
//
//         builder.Property(r => r.Name)
//             .HasColumnName("Name")
//             .HasColumnType("NVARCHAR")
//             .HasMaxLength(120);
//
//         builder.HasIndex(r => r.Id, "IX_Race_Id");
//     }
// }