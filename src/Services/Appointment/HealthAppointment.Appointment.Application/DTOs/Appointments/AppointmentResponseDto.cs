
using HealthAppointment.Appointment.Domain.Enums;

namespace HealthAppointment.Appointment.Application.DTOs.Appointments;

public class AppointmentResponseDto
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid DoctorId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public Guid SpecialtyId { get; set; }

    public AppointmentStatus Status { get; set; }

    public string? Reason { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
