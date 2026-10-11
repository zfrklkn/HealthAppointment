using HealthAppointment.Doctor.Domain.Entities;
using Doc = HealthAppointment.Doctor.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;

namespace HealthAppointment.Doctor.Infrastructure.Persistence;

public class DoctorDbContext : DbContext
{
    public DoctorDbContext(DbContextOptions<DoctorDbContext> options)
        : base(options)
    {
        
    }

    DbSet<Doc> Doctors => Set<Doc>();
    DbSet<DoctorSchedule> DoctorSchedules => Set<DoctorSchedule>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Doc>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(d => d.Biography)
                .HasMaxLength(2000);

            entity.Property(d => d.ProfileImageUrl)
                .HasMaxLength(500);

            entity.Property(d => d.PhoneNumber)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(254);

            entity.Property(d => d.IsActive)
                .IsRequired();
        });


        modelBuilder.Entity<DoctorSchedule>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.DayOfWeek)
                .IsRequired();

            entity.Property(s => s.StartTime)
                .IsRequired();

            entity.Property(s => s.EndTime)
                .IsRequired();

            entity.Property(s => s.IsActive)
                .IsRequired();

            entity.HasIndex(s => new { s.DoctorId, s.DayOfWeek });

            entity.ToTable("DoctorSchedules");

            entity.HasOne<Doc>()
                .WithMany()
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
