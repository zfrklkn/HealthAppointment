
using HealthAppointment.Patient.Application.Features.Patients;
using HealthAppointment.Patient.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HealthAppointment.Patient.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPatientService, PatientService>();

        return services;
    }
}
