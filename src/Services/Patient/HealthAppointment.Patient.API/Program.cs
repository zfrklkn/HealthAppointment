using HealthAppointment.Patient.API.Consumers.Appointments;
using HealthAppointment.Patient.Application;
using HealthAppointment.Patient.Infrastructure;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMassTransit(x =>
{    
    x.AddConsumer<ValidatePatientConsumer>();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
