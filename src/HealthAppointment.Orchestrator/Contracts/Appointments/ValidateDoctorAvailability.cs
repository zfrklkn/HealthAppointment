
namespace HealthAppointment.Orchestrator.Contracts.Appointments;

public sealed record ValidateDoctorAvailability
(
    Guid CorrelationId,
    Guid DoctorId,
    DateTime StartTime,
    DateTime EndTime
);
