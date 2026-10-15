
namespace HealthAppointment.Contracts.Appointments;

public sealed record PatientValidationFailed(
    Guid CorrelationId,
    string Reason);
