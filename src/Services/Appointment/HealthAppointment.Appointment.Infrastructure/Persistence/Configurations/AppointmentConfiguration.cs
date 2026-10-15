
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppointmentEntity = HealthAppointment.Appointment.Domain.Entities.Appointment;

namespace HealthAppointment.Appointment.Infrastructure.Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<AppointmentEntity>
{
    public void Configure(EntityTypeBuilder<AppointmentEntity> builder)
    {
        builder.ToTable("appointments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.PatientId)
            .IsRequired();

        builder.Property(x => x.DoctorId)
            .IsRequired();

        builder.Property(x => x.SpecialtyId)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => new { x.PatientId, x.StartTime });
        builder.HasIndex(x => new { x.DoctorId, x.StartTime });
        builder.HasIndex(x => new { x.DoctorId, x.Status, x.StartTime });
    }
}
