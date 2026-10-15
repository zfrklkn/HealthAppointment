using FluentValidation;
using HealthAppointment.Appointment.Application.Interfaces;
using HealthAppointment.Appointment.Application.Services;
using HealthAppointment.Appointment.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace HealthAppointment.Appointment.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentService, AppointmentService>();

        services.AddValidatorsFromAssemblyContaining<CreateAppointmentRequestDtoValidator>();

        return services;
    }
}