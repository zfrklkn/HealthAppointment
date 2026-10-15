
namespace HealthAppointment.Orchestrator.Contracts.Appointments;

public sealed record AppointmentCreationFailed
(
    Guid CorrelationId,
    string Reason
);
