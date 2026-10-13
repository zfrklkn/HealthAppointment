using HealthAppointment.Doctor.Application.DTOs.Schedules;
using HealthAppointment.Doctor.Application.Interfaces;
using DoctorSchedule = HealthAppointment.Doctor.Domain.Entities.DoctorSchedule;

namespace HealthAppointment.Doctor.Application.Services;

public class DoctorScheduleService : IDoctorScheduleService
{
    private readonly IDoctorScheduleRepository _scheduleRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DoctorScheduleService(
        IDoctorScheduleRepository scheduleRepository,
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork)
    {
        _scheduleRepository = scheduleRepository;
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<DoctorScheduleResponseDto>?> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        var doctor = await _doctorRepository.GetByIdAsync(
            doctorId, cancellationToken);

        if (doctor is null)
            return null;

        var schedules = await _scheduleRepository.GetByDoctorIdAsync(
            doctorId, cancellationToken);

        return schedules.Select(MapToResponse).ToList();
    }

    public async Task<DoctorScheduleResponseDto?> CreateAsync(Guid doctorId, CreateDoctorScheduleRequestDto request, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var doctorLocked = await _doctorRepository
                .LockDoctorForScheduleChangeAsync(
                    doctorId, cancellationToken);

            if (!doctorLocked)
            {
                await _unitOfWork.RollbackTransactionAsync(
                    cancellationToken);

                return null;
            }

            var hasOverlap = await _scheduleRepository.HasOverlapAsync(
                doctorId,
                request.DayOfWeek,
                request.StartTime,
                request.EndTime,
                cancellationToken: cancellationToken);

            if (hasOverlap)
            {
                throw new InvalidOperationException(
                    "Bu gün ve saat aralığında mevcut bir çalışma saatiyle çakışma var.");
            }

            var schedule = new DoctorSchedule
            {
                Id = Guid.NewGuid(),
                DoctorId = doctorId,
                DayOfWeek = request.DayOfWeek,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                IsActive = true
            };

            await _scheduleRepository.AddAsync(
                schedule, cancellationToken);

            await _unitOfWork.CommitTransactionAsync(
                cancellationToken);

            return MapToResponse(schedule);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(
                cancellationToken);

            throw;
        }
    }

    public async Task<DoctorScheduleResponseDto?> UpdateAsync(Guid doctorId, Guid scheduleId, UpdateDoctorScheduleRequestDto request, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var doctorLocked = await _doctorRepository
                .LockDoctorForScheduleChangeAsync(
                    doctorId, cancellationToken);

            if (!doctorLocked)
            {
                await _unitOfWork.RollbackTransactionAsync(
                    cancellationToken);

                return null;
            }

            var schedule = await _scheduleRepository.GetByIdAsync(
                doctorId, scheduleId, cancellationToken);

            if (schedule is null)
            {
                await _unitOfWork.RollbackTransactionAsync(
                    cancellationToken);

                return null;
            }

            var hasOverlap = await _scheduleRepository.HasOverlapAsync(
                doctorId,
                request.DayOfWeek,
                request.StartTime,
                request.EndTime,
                excludeScheduleId: scheduleId,
                cancellationToken: cancellationToken);

            if (hasOverlap)
            {
                throw new InvalidOperationException(
                    "Bu gün ve saat aralığında mevcut bir çalışma saatiyle çakışma var.");
            }

            schedule.DayOfWeek = request.DayOfWeek;
            schedule.StartTime = request.StartTime;
            schedule.EndTime = request.EndTime;

            await _scheduleRepository.UpdateAsync(
                schedule, cancellationToken);

            await _unitOfWork.CommitTransactionAsync(
                cancellationToken);

            return MapToResponse(schedule);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(
                cancellationToken);

            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid doctorId, Guid scheduleId, CancellationToken cancellationToken = default)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(
            doctorId, scheduleId, cancellationToken);

        if (schedule is null)
            return false;

        await _scheduleRepository.DeleteAsync(schedule, cancellationToken);

        return true;
    }

    private static DoctorScheduleResponseDto MapToResponse(
        DoctorSchedule schedule)
    {
        return new DoctorScheduleResponseDto
        {
            Id = schedule.Id,
            DoctorId = schedule.DoctorId,
            DayOfWeek = schedule.DayOfWeek,
            StartTime = schedule.StartTime,
            EndTime = schedule.EndTime,
            IsActive = schedule.IsActive
        };
    }
}

