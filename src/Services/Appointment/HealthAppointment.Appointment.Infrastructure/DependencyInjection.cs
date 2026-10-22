
using HealthAppointment.Appointment.Application.Interfaces;
using HealthAppointment.Appointment.Infrastructure.Messaging;
using HealthAppointment.Appointment.Infrastructure.Persistence;
using HealthAppointment.Appointment.Infrastructure.Repositories;
using HealthAppointment.Contracts.Appointments;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthAppointment.Appointment.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("AppointmentDatabase")
            ?? throw new InvalidOperationException(
                "AppointmentDatabase bağlantı dizesi bulunamadı.");

        services.AddDbContext<AppointmentDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IAppointmentWorkflow, AppointmentWorkflow>();        

        return services;
    }
}
