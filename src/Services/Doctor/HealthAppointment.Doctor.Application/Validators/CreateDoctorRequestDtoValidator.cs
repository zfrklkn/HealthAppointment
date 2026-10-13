
using FluentValidation;
using HealthAppointment.Doctor.Application.DTOs;

namespace HealthAppointment.Doctor.Application.Validators;

public class CreateDoctorRequestDtoValidator : AbstractValidator<CreateDoctorRequestDto>
{
    public CreateDoctorRequestDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Doktorun adı boş olamaz.")
            .MaximumLength(100)
            .WithMessage("Doktorun adı en fazla 100 karakter olabilir.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Doktorun soyadı boş olamaz.")
            .MaximumLength(100)
            .WithMessage("Doktorun soyadı en fazla 100 karakter olabilir.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Doktorun unvanı boş olamaz.")
            .MaximumLength(100)
            .WithMessage("Doktorun unvanı en fazla 100 karakter olabilir.");

        RuleFor(x => x.SpecialtyId)
            .NotEmpty()
            .WithMessage("Uzmanlık alanı seçilmelidir.");

        RuleFor(x => x.Biography)
            .MaximumLength(2000)
            .WithMessage("Biyografi en fazla 2000 karakter olabilir.");

        RuleFor(x => x.ProfileImageUrl)
            .MaximumLength(500)
            .WithMessage("Profil fotoğrafı adresi en fazla 500 karakter olabilir.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Telefon numarası boş olamaz.")
            .MaximumLength(30)
            .WithMessage("Telefon numarası en fazla 30 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-posta adresi boş olamaz.")
            .EmailAddress()
            .WithMessage("Geçerli bir e-posta adresi girilmelidir.")
            .MaximumLength(254)
            .WithMessage("E-posta adresi en fazla 254 karakter olabilir.");
    }
}
