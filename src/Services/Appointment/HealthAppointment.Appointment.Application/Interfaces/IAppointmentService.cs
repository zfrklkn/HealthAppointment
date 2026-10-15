using HealthAppointment.Appointment.Application.DTOs.Appointments;
using HealthAppointment.Appointment.Domain.Enums;

namespace HealthAppointment.Appointment.Application.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AppointmentResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AppointmentResponseDto>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AppointmentResponseDto>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);

    Task<Guid> CreateAsync(CreateAppointmentRequestDto request, CancellationToken cancellationToken = default);

    Task<AppointmentResponseDto?> UpdateStatusAsync(Guid id, AppointmentStatus status, CancellationToken cancellationToken = default);

    Task<AppointmentResponseDto?> RescheduleAsync(Guid id, DateTime startTime, DateTime endTime, CancellationToken cancellationToken = default);
}
