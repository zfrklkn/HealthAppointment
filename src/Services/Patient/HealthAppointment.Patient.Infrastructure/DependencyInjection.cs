
using HealthAppointment.Patient.Application.Interfaces.Repositories;
using HealthAppointment.Patient.Infrastructure.Persistence;
using HealthAppointment.Patient.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthAppointment.Patient.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("PatientDb")
            ?? throw new InvalidOperationException(
                "PatientDb connection string bulunamadı.");

        services.AddDbContext<PatientDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IPatientRepository, PatientRepository>();

        return services;
    }
}
