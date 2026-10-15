using Microsoft.EntityFrameworkCore;
using AppointmentsEntity = HealthAppointment.Appointment.Domain.Entities.Appointment;

namespace HealthAppointment.Appointment.Infrastructure.Persistence;

public class AppointmentDbContext : DbContext
{
    public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
        : base(options)
    {

    }

    public DbSet<AppointmentsEntity> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppointmentDbContext).Assembly);
    }
}
