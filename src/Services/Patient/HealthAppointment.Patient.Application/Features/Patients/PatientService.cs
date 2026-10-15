
using HealthAppointment.Patient.Application.DTOs;
using HealthAppointment.Patient.Application.Interfaces.Repositories;
using HealthAppointment.Patient.Application.Interfaces.Services;
using PatientEntity = HealthAppointment.Patient.Domain.Entities.Patient;

namespace HealthAppointment.Patient.Application.Features.Patients;

public sealed class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientResponseDto> CreateAsync(CreatePatientRequestDto request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var patient = new PatientEntity(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.PhoneNumber,
            request.Email);

        await _patientRepository.AddAsync(patient, cancellationToken);

        return MapToResponse(patient);
    }

    public async Task<PatientResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException(
                "Hasta kimliği boş olamaz.", nameof(id));

        var patient = await _patientRepository.GetByIdAsync(
            id, cancellationToken);

        return patient is null ? null : MapToResponse(patient);
    }

    public async Task<bool> CanMakeAppointmentAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            return false;

        var patient = await _patientRepository.GetByIdAsync(
            id, cancellationToken);

        return patient is not null && patient.IsActive;
    }

    private static PatientResponseDto MapToResponse(PatientEntity patient)
    {
        return new PatientResponseDto(
            patient.Id,
            patient.FirstName,
            patient.LastName,
            patient.DateOfBirth,
            patient.PhoneNumber,
            patient.Email,
            patient.IsActive,
            patient.CreatedAtUtc,
            patient.UpdatedAtUtc);
    }
}
