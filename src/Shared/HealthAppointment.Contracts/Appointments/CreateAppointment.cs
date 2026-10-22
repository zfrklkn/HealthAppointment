namespace HealthAppointment.Contracts.Appointments;

public sealed record CreateAppointment
(
    Guid CorrelationId,
    Guid PatientId,
    Guid DoctorId,
    Guid SpecialtyId,
    DateTime StartTime,
    DateTime EndTime,
    string? Reason
);
