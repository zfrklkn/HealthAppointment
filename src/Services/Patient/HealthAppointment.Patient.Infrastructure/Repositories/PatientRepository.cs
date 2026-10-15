using HealthAppointment.Patient.Application.Interfaces.Repositories;
using HealthAppointment.Patient.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PatientEntity = HealthAppointment.Patient.Domain.Entities.Patient;

namespace HealthAppointment.Patient.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly PatientDbContext _dbContext;

    public PatientRepository(PatientDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(PatientEntity patient, CancellationToken cancellationToken = default)
    {
        await _dbContext.Patients.AddAsync(patient, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Patients            
            .AnyAsync(patient => patient.Id == id, cancellationToken);
    }

    public Task<PatientEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(
            patient => patient.Id == id, cancellationToken);
    }
}
