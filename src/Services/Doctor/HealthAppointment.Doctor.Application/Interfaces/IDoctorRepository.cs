using Doc = HealthAppointment.Doctor.Domain.Entities.Doctor;

namespace HealthAppointment.Doctor.Application.Interfaces;

public interface IDoctorRepository
{
    Task<Doc?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Doc>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Doc doctor,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Doc doctor,
        CancellationToken cancellationToken = default);
}