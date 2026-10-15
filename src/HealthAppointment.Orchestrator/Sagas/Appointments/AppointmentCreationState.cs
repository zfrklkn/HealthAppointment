using MassTransit;

namespace HealthAppointment.Orchestrator.Sagas.Appointments;

public sealed class AppointmentCreationState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public string CurrentState { get; set; } = string.Empty;

    public Guid PatientId { get; set; }

    public Guid DoctorId { get; set; }

    public Guid? SpecialtyId { get; set; }

    public Guid? AppointmentId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Reason { get; set; }

    public string? FailureReason { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}
