using HealthAppointment.Doctor.Application.DTOs;
using HealthAppointment.Doctor.Application.Interfaces;
using DoctorEntity = HealthAppointment.Doctor.Domain.Entities.Doctor;

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

    public async Task<DoctorResponseDto> CreateAsync(CreateDoctorRequestDto request, CancellationToken cancellationToken = default)
    {
        var doctor = new DoctorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Title = request.Title,
            SpecialtyId = request.SpecialtyId,
            Biography = request.Biography,
            ProfileImageUrl = request.ProfileImageUrl,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            IsActive = true
        };

        await _doctorRepository.AddAsync(doctor, cancellationToken);

        return new DoctorResponseDto
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Title = doctor.Title,
            SpecialtyId = doctor.SpecialtyId,
            Biography = doctor.Biography,
            ProfileImageUrl = doctor.ProfileImageUrl,
            IsActive = doctor.IsActive
        };
    }

    public async Task<DoctorResponseDto?> UpdateAsync(Guid id, UpdateDoctorRequestDto request, CancellationToken cancellationToken = default)
    {
        var doctor = await _doctorRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (doctor is null)
        {
            return null;
        }

        doctor.FirstName = request.FirstName;
        doctor.LastName = request.LastName;
        doctor.Title = request.Title;
        doctor.SpecialtyId = request.SpecialtyId;
        doctor.Biography = request.Biography;
        doctor.ProfileImageUrl = request.ProfileImageUrl;
        doctor.PhoneNumber = request.PhoneNumber;
        doctor.Email = request.Email;

        await _doctorRepository.UpdateAsync(
            doctor,
            cancellationToken);

        return new DoctorResponseDto
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Title = doctor.Title,
            SpecialtyId = doctor.SpecialtyId,
            Biography = doctor.Biography,
            ProfileImageUrl = doctor.ProfileImageUrl,
            IsActive = doctor.IsActive
        };
    }

    public async Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var doctor = await _doctorRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (doctor is null)
        {
            return false;
        }

        doctor.IsActive = false;

        await _doctorRepository.UpdateAsync(
            doctor,
            cancellationToken);

        return true;
    }

    public async Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var doctor = await _doctorRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (doctor is null)
        {
            return false;
        }

        doctor.IsActive = true;

        await _doctorRepository.UpdateAsync(
            doctor,
            cancellationToken);

        return true;
    }
}
