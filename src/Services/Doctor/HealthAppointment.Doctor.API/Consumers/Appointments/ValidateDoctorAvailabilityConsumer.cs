using HealthAppointment.Contracts.Appointments;
using HealthAppointment.Doctor.Application.Interfaces;
using MassTransit;

namespace HealthAppointment.Doctor.API.Consumers.Appointments;

public sealed class ValidateDoctorAvailabilityConsumer : IConsumer<ValidateDoctorAvailability>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IDoctorScheduleRepository _scheduleRepository;

    public ValidateDoctorAvailabilityConsumer(IDoctorRepository doctorRepository, IDoctorScheduleRepository scheduleRepository)
    {
        _doctorRepository = doctorRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task Consume(ConsumeContext<ValidateDoctorAvailability> context)
    {
        var message = context.Message;

        var doctor = await _doctorRepository.GetByIdAsync(message.DoctorId, context.CancellationToken);

        if (doctor is null || !doctor.IsActive)
        {
            await context.Publish(new DoctorAvailabilityValidationFailed(
                message.CorrelationId,
                "Doktor bulunamadı veya aktif değil"));

            return;
        }

        if (message.EndTime <= message.StartTime || message.StartTime.Date != message.EndTime.Date)
        {
            await context.Publish(new DoctorAvailabilityValidationFailed(
                message.CorrelationId,
                "Randevu geçerli bir zaman aralığında ve aynı gün içinde olmalıdır."));

            return;
        }

        var schedules = await _scheduleRepository.GetByDoctorIdAsync(message.DoctorId, context.CancellationToken);

        var dayOfWeek = message.StartTime.DayOfWeek;
        var startTime = TimeOnly.FromDateTime(message.StartTime);
        var endTime = TimeOnly.FromDateTime(message.EndTime);

        var isAvailable = schedules.Any(schedule =>
            schedule.IsActive &&
            schedule.DayOfWeek == dayOfWeek &&
            schedule.StartTime <= startTime &&
            schedule.EndTime >= endTime);

        if (!isAvailable)
        {
            await context.Publish(new DoctorAvailabilityValidationFailed(
                message.CorrelationId,
                "Doktor istenen gün veya saat aralığında çalışmıyor."));

            return;
        }

        await context.Publish(new DoctorAvailabilityValidated(
            message.CorrelationId,
            doctor.Id,
            doctor.SpecialtyId));
    }
}
