using HealthAppointment.Appointment.Application.DTOs.Appointments;

namespace HealthAppointment.Appointment.Application.Interfaces;

public interface IAppointmentWorkflow
{
    Task<Guid> StartAppointmentAsync(CreateAppointmentRequestDto request, CancellationToken cancellationToken = default);
}
