
using HealthAppointment.Doctor.Application.Interfaces;
using HealthAppointment.Doctor.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using DoctorEntity = HealthAppointment.Doctor.Domain.Entities.Doctor;

namespace HealthAppointment.Doctor.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly DoctorDbContext _context;

    public DoctorRepository(DoctorDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(
                doctor => doctor.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<DoctorEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AsNoTracking()
            .OrderBy(doctor => doctor.LastName)
            .ThenBy(doctor => doctor.FirstName)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        DoctorEntity doctor,
        CancellationToken cancellationToken = default)
    {
        await _context.Doctors.AddAsync(doctor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        DoctorEntity doctor,
        CancellationToken cancellationToken = default)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> LockDoctorForScheduleChangeAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        var doctor = await _context.Doctors
            .FromSqlInterpolated(
                $"SELECT * FROM Doctors WHERE Id = {doctorId} FOR UPDATE")
            .FirstOrDefaultAsync(cancellationToken);

        return doctor is not null;
    }
}