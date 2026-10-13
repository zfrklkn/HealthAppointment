
using HealthAppointment.Doctor.Application.Interfaces;
using HealthAppointment.Doctor.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using DoctorSchedule = HealthAppointment.Doctor.Domain.Entities.DoctorSchedule;

namespace HealthAppointment.Doctor.Infrastructure.Repositories;

public class DoctorScheduleRepository : IDoctorScheduleRepository
{
    private readonly DoctorDbContext _context;

    public DoctorScheduleRepository(DoctorDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorSchedule?> GetByIdAsync(Guid doctorId, Guid scheduleId, CancellationToken cancellationToken = default)
    {
        return await _context.DoctorSchedules
            .FirstOrDefaultAsync(
                schedule => schedule.Id == scheduleId
                    && schedule.DoctorId == doctorId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<DoctorSchedule>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.DoctorSchedules
            .AsNoTracking()
            .Where(schedule => schedule.DoctorId == doctorId)
            .OrderBy(schedule => schedule.DayOfWeek)
            .ThenBy(schedule => schedule.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DoctorSchedule schedule, CancellationToken cancellationToken = default)
    {
        await _context.DoctorSchedules.AddAsync(
            schedule,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DoctorSchedule schedule, CancellationToken cancellationToken = default)
    {
        _context.DoctorSchedules.Update(schedule);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DoctorSchedule schedule, CancellationToken cancellationToken = default)
    {
        _context.DoctorSchedules.Remove(schedule);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> HasOverlapAsync(Guid doctorId, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime, Guid? excludeScheduleId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.DoctorSchedules
            .AsNoTracking()
            .Where(schedule =>
                schedule.DoctorId == doctorId
                && schedule.DayOfWeek == dayOfWeek
                && schedule.IsActive
                && schedule.StartTime < endTime
                && schedule.EndTime > startTime);

        if (excludeScheduleId.HasValue)
        {
            query = query.Where(
                schedule => schedule.Id != excludeScheduleId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
}