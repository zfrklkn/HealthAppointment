
namespace HealthAppointment.Orchestrator.Contracts.Appointments;

public sealed record CreateAppointment
(
    Guid CorrelationId,
    Guid PatientId,
    Guid DoctorId,
    Guid SpecialityId,
    DateTime StartDate,
    DateTime EndDate,
    string? Reason
);
