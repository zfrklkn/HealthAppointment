
namespace HealthAppointment.Patient.Application.DTOs;

public sealed record CreatePatientRequestDto
(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string? PhoneNumber,
    string? Email
);