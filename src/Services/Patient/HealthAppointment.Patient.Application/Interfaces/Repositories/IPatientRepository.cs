
using PatientEntity = HealthAppointment.Patient.Domain.Entities.Patient;

namespace HealthAppointment.Patient.Application.Interfaces.Repositories;

public interface IPatientRepository
{
    Task<PatientEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(PatientEntity patient, CancellationToken cancellationToken = default);
}
