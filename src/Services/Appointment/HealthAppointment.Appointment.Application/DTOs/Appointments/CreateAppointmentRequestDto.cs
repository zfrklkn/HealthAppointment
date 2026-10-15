namespace HealthAppointment.Appointment.Application.DTOs.Appointments;

public class CreateAppointmentRequestDto
{
    public Guid PatientId { get; set; }

    public Guid DoctorId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Reason { get; set; }
}
