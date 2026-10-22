using AppointmentEntity = HealthAppointment.Appointment.Domain.Entities.Appointment;

namespace HealthAppointment.Appointment.Application.Interfaces;

public interface IAppointmentRepository
{
    Task<AppointmentEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AppointmentEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AppointmentEntity>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AppointmentEntity>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);

    Task AddAsync(AppointmentEntity appointment, CancellationToken cancellationToken = default);

    Task UpdateAsync(AppointmentEntity appointment, CancellationToken cancellationToken = default);

    Task<AppointmentEntity?> GetByCreationCorrelationIdAsync(Guid correlationId, CancellationToken cancellationToken = default);
}
