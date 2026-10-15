
namespace HealthAppointment.Patient.Domain.Entities;

public sealed class Patient
{
    private Patient()
    {
        // EF Core için.
    }

    public Patient(
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        string? phoneNumber = null,
        string? email = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(
                "Ad boş olamaz.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException(
                "Soyad boş olamaz.", nameof(lastName));

        if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException(
                "Doğum tarihi gelecekte olamaz.", nameof(dateOfBirth));

        Id = Guid.NewGuid();
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        DateOfBirth = dateOfBirth;
        PhoneNumber = NormalizeOptional(phoneNumber);
        Email = NormalizeOptional(email);
        CreatedAtUtc = DateTime.UtcNow;
        IsActive = true;
    }

    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public DateOnly DateOfBirth { get; private set; }

    public string? PhoneNumber { get; private set; }

    public string? Email { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? UpdatedAtUtc { get; private set; }

    public string FullName => $"{FirstName} {LastName}";

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }
}
