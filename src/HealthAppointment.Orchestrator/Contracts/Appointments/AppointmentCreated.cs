
namespace HealthAppointment.Orchestrator.Contracts.Appointments;

public sealed record AppointmentCreated
(
    Guid CorrelationId,
    Guid AppointmentId
);
