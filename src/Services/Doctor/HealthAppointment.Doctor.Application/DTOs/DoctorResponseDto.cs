namespace HealthAppointment.Doctor.Application.DTOs;

public class DoctorResponseDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Title { get; set; }

    public Guid SpecialtyId { get; set; }

    public string? Biography { get; set; }

    public string? ProfileImageUrl { get; set; }

    public bool IsActive { get; set; }
}
