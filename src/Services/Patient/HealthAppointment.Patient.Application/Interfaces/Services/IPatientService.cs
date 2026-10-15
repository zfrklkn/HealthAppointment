
using HealthAppointment.Patient.Application.DTOs;

namespace HealthAppointment.Patient.Application.Interfaces.Services;

public interface IPatientService
{
    Task<PatientResponseDto> CreateAsync(CreatePatientRequestDto request, CancellationToken cancellationToken = default);

    Task<PatientResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> CanMakeAppointmentAsync(Guid id, CancellationToken cancellationToken = default);
}
