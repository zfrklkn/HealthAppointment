
using HealthAppointment.Orchestrator.Persistence;
using HealthAppointment.Orchestrator.Sagas.Appointments;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("OrchestratorDb")
    ?? throw new InvalidOperationException(
        "OrchestratorDb connection string bulunamadı.");

builder.Services.AddDbContext<OrchestratorDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddMassTransit(x =>
{
    x.AddSagaStateMachine<
        AppointmentCreationStateMachine,
        AppointmentCreationState>()
        .EntityFrameworkRepository(r =>
        {
            r.ExistingDbContext<OrchestratorDbContext>();
            r.UsePostgres();
        });

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqHost =
            builder.Configuration["RabbitMQ:Host"] ?? "localhost";

        var virtualHost =
            builder.Configuration["RabbitMQ:VirtualHost"] ?? "/";

        var username =
            builder.Configuration["RabbitMQ:Username"]
            ?? throw new InvalidOperationException(
                "RabbitMQ Username bulunamadı.");

        var password =
            builder.Configuration["RabbitMQ:Password"]
            ?? throw new InvalidOperationException(
                "RabbitMQ Password bulunamadı.");

        cfg.Host(rabbitMqHost, virtualHost, h =>
        {
            h.Username(username);
            h.Password(password);
        });

        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();

host.Run();
