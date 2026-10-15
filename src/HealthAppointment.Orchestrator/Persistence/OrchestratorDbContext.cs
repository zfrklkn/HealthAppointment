using HealthAppointment.Orchestrator.Persistence.Configurations;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace HealthAppointment.Orchestrator.Persistence;

public sealed class OrchestratorDbContext : SagaDbContext
{
    public OrchestratorDbContext(DbContextOptions<OrchestratorDbContext> options) : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {
            yield return new AppointmentCreationStateMap();
        }
    }
}
