
namespace HealthAppointment.Orchestrator.Contracts.Appointments;

public sealed record DoctorAvailabilityValidated
(
    Guid CorrelationId,
    Guid DoctorId,
    Guid SpecialtyId
);
