using HealthAppointment.Appointment.Application.DTOs.Appointments;
using HealthAppointment.Appointment.Application.Interfaces;
using HealthAppointment.Contracts.Appointments;
using MassTransit;

namespace HealthAppointment.Appointment.Infrastructure.Messaging;

public class AppointmentWorkflow : IAppointmentWorkflow
{
    private readonly IPublishEndpoint _publishEndpoint;

    public AppointmentWorkflow(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> StartAppointmentAsync(CreateAppointmentRequestDto request, CancellationToken cancellationToken = default)
    {
        var correlationId = Guid.NewGuid();

        await _publishEndpoint.Publish(
            new StartAppointment(
                correlationId,
                request.PatientId,
                request.DoctorId,
                request.StartTime,
                request.EndTime,
                request.Reason),
            cancellationToken);

        return correlationId;
    }
}
