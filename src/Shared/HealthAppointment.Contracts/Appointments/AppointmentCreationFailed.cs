
namespace HealthAppointment.Contracts.Appointments;

public sealed record AppointmentCreationFailed
(
    Guid CorrelationId,
    string Reason
);
