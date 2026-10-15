
namespace HealthAppointment.Orchestrator.Contracts.Appointments;

public sealed record DoctorAvailabilityValidationFailed
(
    Guid CorrelationId,
    string Reason
);
