
using HealthAppointment.Orchestrator.Sagas.Appointments;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthAppointment.Orchestrator.Persistence.Configurations;

public sealed class AppointmentCreationStateMap : SagaClassMap<AppointmentCreationState>
{
    protected override void Configure(EntityTypeBuilder<AppointmentCreationState> entity, ModelBuilder model)
    {
        entity.ToTable("AppointmentCreationStates");

        entity.Property(x => x.CurrentState)
            .HasMaxLength(64)
            .IsRequired();

        entity.Property(x => x.PatientId).IsRequired();
        entity.Property(x => x.DoctorId).IsRequired();
        entity.Property(x => x.SpecialtyId);
        entity.Property(x => x.AppointmentId);
        entity.Property(x => x.StartTime).IsRequired();
        entity.Property(x => x.EndTime).IsRequired();

        entity.Property(x => x.Reason).HasMaxLength(1000);
        entity.Property(x => x.FailureReason).HasMaxLength(2000);
        entity.Property(x => x.CreatedAtUtc).IsRequired();

        entity.HasIndex(x => x.CurrentState);
        entity.HasIndex(x => x.CreatedAtUtc);
    }
}
