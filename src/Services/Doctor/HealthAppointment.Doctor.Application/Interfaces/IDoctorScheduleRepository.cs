
using DoctorSchedule = HealthAppointment.Doctor.Domain.Entities.DoctorSchedule;

namespace HealthAppointment.Doctor.Application.Interfaces;

public interface IDoctorScheduleRepository
{
    Task<DoctorSchedule?> GetByIdAsync(
        Guid doctorId,
        Guid scheduleId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoctorSchedule>> GetByDoctorIdAsync(
        Guid doctorId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        DoctorSchedule schedule,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        DoctorSchedule schedule,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        DoctorSchedule schedule,
        CancellationToken cancellationToken = default);

    Task<bool> HasOverlapAsync(
        Guid doctorId,
        DayOfWeek dayOfWeek,
        TimeOnly startTime,
        TimeOnly endTime,
        Guid? excludeScheduleId = null,
        CancellationToken cancellationToken = default);
}