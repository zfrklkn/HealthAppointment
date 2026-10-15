
namespace HealthAppointment.Contracts.Appointments;

public sealed record StartAppointment
(
    Guid CorrelationId,
    Guid PatientId,
    Guid DoctorId,
    DateTime StartTime,
    DateTime EndTime,
    string? Reason
);
