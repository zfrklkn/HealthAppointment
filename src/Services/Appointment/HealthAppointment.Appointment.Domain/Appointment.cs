
using HealthAppointment.Appointment.Domain.Enums;

namespace HealthAppointment.Appointment.Domain.Entities;

public class Appointment
{
    private Appointment()
    {
        // For EF Core
    }

    public Appointment(Guid patientId, Guid doctorId, Guid specialtyId, DateTime startTime, DateTime endTime, string? reason = null)
    {
        if (patientId == Guid.Empty)
            throw new ArgumentException("PatientId boş olamaz.", nameof(patientId));

        if (doctorId == Guid.Empty)
            throw new ArgumentException("DoctorId boş olamaz.", nameof(doctorId));

        if (specialtyId == Guid.Empty)
            throw new ArgumentException("SpecialtyId boş olamaz.", nameof(specialtyId));

        if (endTime <= startTime)
            throw new ArgumentException("Randevu bitiş zamanı başlangıç zamanından sonra olmalıdır.");

        Id = Guid.NewGuid();
        PatientId = patientId;
        DoctorId = doctorId;
        SpecialtyId = specialtyId;
        StartTime = startTime;
        EndTime = endTime;
        Reason = reason;
        Status = AppointmentStatus.Confirmed;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid PatientId { get; private set; }

    public Guid DoctorId { get; private set; }

    public Guid SpecialtyId { get; private set; }

    public DateTime StartTime { get; private set; }

    public DateTime EndTime { get; private set; }

    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Confirmed;

    public string? Reason { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public bool HasValidTimeRange()
    {
        return EndTime > StartTime;
    }

    public bool CanChangeStatus(AppointmentStatus newStatus)
    {
        return Status switch
        {
            AppointmentStatus.Confirmed =>
                newStatus is AppointmentStatus.Cancelled
                    or AppointmentStatus.Completed
                    or AppointmentStatus.NoShow,
            _ => false
        };
    }

    public bool ChangeStatus(AppointmentStatus newStatus)
    {
        if (!CanChangeStatus(newStatus))
        {
            return false;
        }

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;

        return true;
    }

    public bool ChangeTimeRange(DateTime startTime, DateTime endTime)
    {
        if (endTime <= startTime)
        {
            return false;
        }

        StartTime = startTime;
        EndTime = endTime;
        UpdatedAt = DateTime.UtcNow;

        return true;
    }
}
