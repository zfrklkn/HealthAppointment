using Microsoft.EntityFrameworkCore;
using PatientEntity = HealthAppointment.Patient.Domain.Entities.Patient;

namespace HealthAppointment.Patient.Infrastructure.Persistence;

public class PatientDbContext : DbContext
{
    public PatientDbContext(DbContextOptions<PatientDbContext> options)
        : base(options)
    {

    }

    public DbSet<PatientEntity> Patients => Set<PatientEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatientDbContext).Assembly);

    }
}
