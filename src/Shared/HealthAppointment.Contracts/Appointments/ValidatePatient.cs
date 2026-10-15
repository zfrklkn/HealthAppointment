
namespace HealthAppointment.Contracts.Appointments;

public sealed record ValidatePatient(
    Guid CorrelationId,
    Guid PatientId);
