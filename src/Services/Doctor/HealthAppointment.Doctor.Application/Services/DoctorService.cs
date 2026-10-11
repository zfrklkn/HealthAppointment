using HealthAppointment.Doctor.Application.DTOs;
using HealthAppointment.Doctor.Application.Interfaces;

namespace HealthAppointment.Doctor.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<IReadOnlyList<DoctorResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var doctors = await _doctorRepository.GetAllAsync(cancellationToken);

        return doctors.Select(doctor => new DoctorResponseDto
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Biography = doctor.Biography,
            ProfileImageUrl = doctor.ProfileImageUrl,
            Title = doctor.Title,
            SpecialtyId = doctor.SpecialtyId,
            IsActive = doctor.IsActive
        }).ToList();
    }

    public async Task<DoctorResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id, cancellationToken);

        if (doctor is null)
        {
            return null;
        }

        return new DoctorResponseDto
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Biography = doctor.Biography,
            ProfileImageUrl = doctor.ProfileImageUrl,
            Title = doctor.Title,
            SpecialtyId = doctor.SpecialtyId,
            IsActive = doctor.IsActive
        };
    }
}
