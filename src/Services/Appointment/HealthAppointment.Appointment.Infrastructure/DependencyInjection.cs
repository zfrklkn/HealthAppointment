
using HealthAppointment.Appointment.Application.Interfaces;
using HealthAppointment.Appointment.Infrastructure.Messaging;
using HealthAppointment.Appointment.Infrastructure.Persistence;
using HealthAppointment.Appointment.Infrastructure.Repositories;
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

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMq = configuration.GetSection("RabbitMQ");

                var host = rabbitMq["Host"]
                    ?? throw new InvalidOperationException(
                        "RabbitMQ:Host ayarı bulunamadı.");

                var virtualHost = rabbitMq["VirtualHost"] ?? "/";
                var username = rabbitMq["Username"]
                    ?? throw new InvalidOperationException(
                        "RabbitMQ:Username ayarı bulunamadı.");
                var password = rabbitMq["Password"]
                    ?? throw new InvalidOperationException(
                        "RabbitMQ:Password ayarı bulunamadı.");

                cfg.Host(host, virtualHost, h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
            });
        });

        return services;
    }
}
