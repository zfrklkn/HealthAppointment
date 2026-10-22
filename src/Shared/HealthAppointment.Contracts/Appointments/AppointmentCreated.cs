namespace HealthAppointment.Contracts.Appointments;

public sealed record AppointmentCreated
(
    Guid CorrelationId,
    Guid AppointmentId
);