using FluentValidation;
using HealthAppointment.Doctor.Application.DTOs.Schedules;
using HealthAppointment.Doctor.Application.Interfaces;
using HealthAppointment.Doctor.Application.Services;
using HealthAppointment.Doctor.Application.Validators;
using HealthAppointment.Doctor.Infrastructure.Persistence;
using HealthAppointment.Doctor.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DoctorDb");

builder.Services.AddDbContext<DoctorDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)));

// FluentValidation setup
builder.Services.AddValidatorsFromAssemblyContaining<CreateDoctorRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateDoctorRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateDoctorScheduleRequestDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateDoctorScheduleRequestDto>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();
builder.Services.AddScoped<IDoctorScheduleService, DoctorScheduleService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
