
using HealthAppointment.Contracts.Appointments;
using MassTransit;

namespace HealthAppointment.Orchestrator.Sagas.Appointments;

public sealed class AppointmentCreationStateMachine : MassTransitStateMachine<AppointmentCreationState>
{
    public State ValidatingPatient { get; private set; } = null!;
    public State ValidatingDoctor { get; private set; } = null!;
    public State CreatingAppointment { get; private set; } = null!;
    public State Completed { get; private set; } = null!;
    public State Failed { get; private set; } = null!;

    public Event<StartAppointment> StartAppointment { get; private set; } = null!;
    public Event<PatientValidated> PatientValidated { get; private set; } = null!;
    public Event<PatientValidationFailed> PatientValidationFailed { get; private set; } = null!;
    public Event<DoctorAvailabilityValidated> DoctorAvailabilityValidated { get; private set; } = null!;
    public Event<DoctorAvailabilityValidationFailed> DoctorAvailabilityValidationFailed { get; private set; } = null!;
    public Event<AppointmentCreated> AppointmentCreated { get; private set; } = null!;
    public Event<AppointmentCreationFailed> AppointmentCreationFailed { get; private set; } = null!;

    public AppointmentCreationStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => StartAppointment,
            x => x.CorrelateById(context => context.Message.CorrelationId));

        Event(() => PatientValidated,
            x => x.CorrelateById(context => context.Message.CorrelationId));

        Event(() => PatientValidationFailed,
            x => x.CorrelateById(context => context.Message.CorrelationId));

        Event(() => DoctorAvailabilityValidated,
            x => x.CorrelateById(context => context.Message.CorrelationId));

        Event(() => DoctorAvailabilityValidationFailed,
            x => x.CorrelateById(context => context.Message.CorrelationId));

        Event(() => AppointmentCreated,
            x => x.CorrelateById(context => context.Message.CorrelationId));

        Event(() => AppointmentCreationFailed,
            x => x.CorrelateById(context => context.Message.CorrelationId));

        Initially(
            When(StartAppointment)
                .Then(context =>
                {
                    context.Saga.PatientId = context.Message.PatientId;
                    context.Saga.DoctorId = context.Message.DoctorId;
                    context.Saga.StartTime = context.Message.StartTime;
                    context.Saga.EndTime = context.Message.EndTime;
                    context.Saga.Reason = context.Message.Reason;
                    context.Saga.CreatedAtUtc = DateTime.UtcNow;
                })
                .TransitionTo(ValidatingPatient)
                .Publish(context => new ValidatePatient(
                    context.Saga.CorrelationId,
                    context.Saga.PatientId))
        );

        During(ValidatingPatient,
            When(PatientValidated)
                .TransitionTo(ValidatingDoctor)
                .Publish(context => new ValidateDoctorAvailability(
                    context.Saga.CorrelationId,
                    context.Saga.DoctorId,
                    context.Saga.StartTime,
                    context.Saga.EndTime)),

            When(PatientValidationFailed)
                .Then(context =>
                    context.Saga.FailureReason = context.Message.Reason)
                .TransitionTo(Failed)
        );

        During(ValidatingDoctor,
            When(DoctorAvailabilityValidated)
                .Then(context =>
                    context.Saga.SpecialtyId = context.Message.SpecialtyId)
                .TransitionTo(CreatingAppointment)
                .Publish(context => new CreateAppointment(
                    context.Saga.CorrelationId,
                    context.Saga.PatientId,
                    context.Saga.DoctorId,
                    context.Saga.SpecialtyId!.Value,
                    context.Saga.StartTime,
                    context.Saga.EndTime,
                    context.Saga.Reason)),

            When(DoctorAvailabilityValidationFailed)
                .Then(context =>
                    context.Saga.FailureReason = context.Message.Reason)
                .TransitionTo(Failed)
        );

        During(CreatingAppointment,
            When(AppointmentCreated)
                .Then(context =>
                    context.Saga.AppointmentId = context.Message.AppointmentId)
                .TransitionTo(Completed),

            When(AppointmentCreationFailed)
                .Then(context =>
                    context.Saga.FailureReason = context.Message.Reason)
                .TransitionTo(Failed)
        );
    }
}
