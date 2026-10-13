using HealthAppointment.Doctor.Application.DTOs.Schedules;

namespace HealthAppointment.Doctor.Application.Interfaces;

public interface IDoctorScheduleService
{
    Task<IReadOnlyList<DoctorScheduleResponseDto>?> GetByDoctorIdAsync(
        Guid doctorId,
        CancellationToken cancellationToken = default);

    Task<DoctorScheduleResponseDto?> CreateAsync(
        Guid doctorId,
        CreateDoctorScheduleRequestDto request,
        CancellationToken cancellationToken = default);

    Task<DoctorScheduleResponseDto?> UpdateAsync(
        Guid doctorId,
        Guid scheduleId,
        UpdateDoctorScheduleRequestDto request,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        Guid doctorId,
        Guid scheduleId,
        CancellationToken cancellationToken = default);
}
