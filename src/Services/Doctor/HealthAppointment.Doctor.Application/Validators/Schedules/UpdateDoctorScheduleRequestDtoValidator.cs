using FluentValidation;
using HealthAppointment.Doctor.Application.DTOs.Schedules;


namespace HealthAppointment.Doctor.Application.Validators.Schedules;

public class UpdateDoctorScheduleRequestDtoValidator : AbstractValidator<UpdateDoctorScheduleRequestDto>
{
    public UpdateDoctorScheduleRequestDtoValidator()
    {
        RuleFor(x => x.DayOfWeek)
            .IsInEnum()
            .WithMessage("Geçerli bir haftanın günü seçilmelidir.");

        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime)
            .WithMessage("Başlangıç saati bitiş saatinden önce olmalıdır.");
    }
}
