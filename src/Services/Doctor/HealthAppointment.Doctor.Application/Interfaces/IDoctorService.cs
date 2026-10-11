using HealthAppointment.Doctor.Application.DTOs;

namespace HealthAppointment.Doctor.Application.Interfaces;

public interface IDoctorService
{
    Task<DoctorResponseDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoctorResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default);
}
