
namespace HealthAppointment.Appointment.Application.Messages.Appointments;

public sealed class StartAppointmentRequest(
    Guid CorrelationId,
    Guid PatientId,
    Guid DoctorId,
    DateTime StartTime,
    DateTime EndTime,
    string? Reason);
