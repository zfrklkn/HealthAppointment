using HealthAppointment.Appointment.API.Consumers.Appointments;
using HealthAppointment.Appointment.Application;
using HealthAppointment.Appointment.Infrastructure;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateAppointmentConsumer>();

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

        cfg.UseMessageRetry(r =>
        {
            r.Intervals(
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(5));
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
