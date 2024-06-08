using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Mappings;

public class ScheduleMap : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedule");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn()
            .IsRequired();

        builder.Property(s => s.Date)
            .HasColumnName("Date")
            .HasColumnType("DATETIME")
            .IsRequired();
        
        builder.Property(s => s.Time)
            .HasColumnName("Time")
            .HasColumnType("TIME")
            .IsRequired();

        builder.Property(s => s.Observation)
            .HasColumnName("Observation")
            .HasColumnType("VARCHAR(255)");

        builder.HasOne(s => s.Animal)
            .WithMany(s => s.Schedules)
            .HasForeignKey(s => s.AnimalId)
            .HasConstraintName("FK_Schedule_Animal_AnimalId")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(s => s.Service)
            .WithMany(s => s.Schedules)
            .HasForeignKey(s => s.ServiceId)
            .HasConstraintName("FK_Schedule_Animal_ServiceId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(s => s.Id, "IX_Schedule_Id")
            .IsUnique();
    }
    
}