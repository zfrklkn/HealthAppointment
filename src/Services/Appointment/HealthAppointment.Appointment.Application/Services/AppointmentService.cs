using HealthAppointment.Appointment.Application.DTOs.Appointments;
using HealthAppointment.Appointment.Application.Interfaces;
using HealthAppointment.Appointment.Domain.Enums;
using AppointmentEntity = HealthAppointment.Appointment.Domain.Entities.Appointment;

namespace HealthAppointment.Appointment.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppointmentWorkflow _appointmentWorkflow;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork,
        IAppointmentWorkflow appointmentWorkflow)
    {
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
        _appointmentWorkflow = appointmentWorkflow;
    }

public async Task<AppointmentResponseDto?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(
            id,
            cancellationToken);

        return appointment is null
            ? null
            : MapToResponseDto(appointment);
    }

    public async Task<IReadOnlyList<AppointmentResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var appointments = await _appointmentRepository.GetAllAsync(
            cancellationToken);

        return appointments
            .Select(MapToResponseDto)
            .ToList();
    }

    public async Task<IReadOnlyList<AppointmentResponseDto>> GetByPatientIdAsync(
        Guid patientId,
        CancellationToken cancellationToken = default)
    {
        var appointments = await _appointmentRepository.GetByPatientIdAsync(
            patientId,
            cancellationToken);

        return appointments
            .Select(MapToResponseDto)
            .ToList();
    }

    public async Task<IReadOnlyList<AppointmentResponseDto>> GetByDoctorIdAsync(
        Guid doctorId,
        CancellationToken cancellationToken = default)
    {
        var appointments = await _appointmentRepository.GetByDoctorIdAsync(
            doctorId,
            cancellationToken);

        return appointments
            .Select(MapToResponseDto)
            .ToList();
    }

    private static AppointmentResponseDto MapToResponseDto(
        AppointmentEntity appointment)
    {
        return new AppointmentResponseDto
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            DoctorId = appointment.DoctorId,
            SpecialtyId = appointment.SpecialtyId,
            StartTime = appointment.StartTime,
            EndTime = appointment.EndTime,
            Status = appointment.Status,
            Reason = appointment.Reason,
            CreatedAt = appointment.CreatedAt,
            UpdatedAt = appointment.UpdatedAt
        };
    }

    public async Task<Guid> CreateAsync(CreateAppointmentRequestDto request, CancellationToken cancellationToken = default)
    {
        return await _appointmentWorkflow.StartAppointmentAsync(request, cancellationToken);
    }

    public Task<AppointmentResponseDto?> UpdateStatusAsync(Guid id, AppointmentStatus status, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentResponseDto?> RescheduleAsync(Guid id, DateTime startTime, DateTime endTime, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
