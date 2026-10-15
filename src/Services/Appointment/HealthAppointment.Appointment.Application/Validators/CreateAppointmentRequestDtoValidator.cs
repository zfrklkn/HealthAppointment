
using FluentValidation;
using HealthAppointment.Appointment.Application.DTOs.Appointments;

namespace HealthAppointment.Appointment.Application.Validators;

public class CreateAppointmentRequestDtoValidator : AbstractValidator<CreateAppointmentRequestDto>
{
    public CreateAppointmentRequestDtoValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty()
            .WithMessage("Hasta seçimi zorunludur.");

        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage("Doktor seçimi zorunludur.");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("Randevu başlangıç zamanı zorunludur.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("Randevu bitiş zamanı başlangıç zamanından sonra olmalıdır.");

        RuleFor(x => x.Reason)
            .MaximumLength(1000)
            .WithMessage("Randevu nedeni en fazla 1000 karakter olabilir.");
    }
}
