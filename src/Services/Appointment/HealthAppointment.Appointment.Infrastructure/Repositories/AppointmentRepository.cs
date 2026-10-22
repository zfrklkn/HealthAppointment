using HealthAppointment.Appointment.Application.Interfaces;
using HealthAppointment.Appointment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using AppointmentEntity = HealthAppointment.Appointment.Domain.Entities.Appointment;

namespace HealthAppointment.Appointment.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppointmentDbContext _context;

    public AppointmentRepository(AppointmentDbContext context)
    {
        _context = context;
    }

    public async Task<AppointmentEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(
                appointment => appointment.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<AppointmentEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AsNoTracking()
            .OrderByDescending(appointment => appointment.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<AppointmentEntity>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Where(appointment => appointment.PatientId == patientId)
            .OrderBy(appointment => appointment.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<AppointmentEntity>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Where(appointment => appointment.DoctorId == doctorId)
            .OrderBy(appointment => appointment.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AppointmentEntity appointment, CancellationToken cancellationToken = default)
    {
        await _context.Appointments.AddAsync(
            appointment,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AppointmentEntity appointment, CancellationToken cancellationToken = default)
    {
        _context.Appointments.Update(appointment);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<AppointmentEntity?> GetByCreationCorrelationIdAsync(Guid correlationId, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.CreationCorrelationId == correlationId,
                cancellationToken);
    }
}