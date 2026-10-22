
namespace HealthAppointment.Contracts.Appointments;

public sealed record DoctorAvailabilityValidationFailed
(
    Guid CorrelationId,
    string Reason
);
