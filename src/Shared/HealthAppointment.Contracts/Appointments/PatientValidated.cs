
namespace HealthAppointment.Contracts.Appointments;

public sealed record PatientValidated(
    Guid CorrelationId,
    Guid PatientId);
