
namespace HealthAppointment.Patient.Application.DTOs;

public sealed record PatientResponseDto
(
    Guid Id,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string? PhoneNumber,
    string? Email,
    bool IsActive,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc
);
