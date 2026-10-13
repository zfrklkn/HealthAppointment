using HealthAppointment.Doctor.Application.DTOs;

namespace HealthAppointment.Doctor.Application.Interfaces;

public interface IDoctorService
{
    Task<DoctorResponseDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoctorResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<DoctorResponseDto> CreateAsync(
        CreateDoctorRequestDto request,
        CancellationToken cancellationToken = default);

    Task<DoctorResponseDto?> UpdateAsync(
        Guid id,
        UpdateDoctorRequestDto request,
        CancellationToken cancellationToken = default);

    Task<bool> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<bool> ActivateAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
