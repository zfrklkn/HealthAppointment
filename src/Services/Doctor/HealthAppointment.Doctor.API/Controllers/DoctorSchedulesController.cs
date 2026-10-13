using FluentValidation;
using HealthAppointment.Doctor.Application.DTOs.Schedules;
using HealthAppointment.Doctor.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthAppointment.Doctor.API.Controllers;

[ApiController]
[Route("api/doctors/{doctorId:guid}/schedules")]
public class DoctorSchedulesController : ControllerBase
{
    private readonly IDoctorScheduleService _scheduleService;
    private readonly IValidator<CreateDoctorScheduleRequestDto> _createValidator;
    private readonly IValidator<UpdateDoctorScheduleRequestDto> _updateValidator;

    public DoctorSchedulesController(IDoctorScheduleService scheduleService, IValidator<CreateDoctorScheduleRequestDto> createValidator, IValidator<UpdateDoctorScheduleRequestDto> updateValidator)
    {
        _scheduleService = scheduleService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetByDoctorId(Guid doctorId, CancellationToken cancellationToken)
    {
        var schedules = await _scheduleService.GetByDoctorIdAsync(
            doctorId, cancellationToken);

        if (schedules is null)
            return NotFound(new { message = "Doktor bulunamadı." });

        return Ok(schedules);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid doctorId, [FromBody] CreateDoctorScheduleRequestDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _createValidator.ValidateAsync(
            request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray());

            return BadRequest(new
            {
                message = "İstek doğrulanamadı.",
                errors
            });
        }

        try
        {
            var schedule = await _scheduleService.CreateAsync(
                doctorId, request, cancellationToken);

            if (schedule is null)
                return NotFound(new { message = "Doktor bulunamadı." });

            return CreatedAtAction(
                nameof(GetByDoctorId),
                new { doctorId },
                schedule);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpPut("{scheduleId:guid}")]
    public async Task<IActionResult> Update(Guid doctorId, Guid scheduleId, [FromBody] UpdateDoctorScheduleRequestDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _updateValidator.ValidateAsync(
            request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray());

            return BadRequest(new
            {
                message = "İstek doğrulanamadı.",
                errors
            });
        }

        try
        {
            var schedule = await _scheduleService.UpdateAsync(
                doctorId, scheduleId, request, cancellationToken);

            if (schedule is null)
                return NotFound(new
                {
                    message = "Doktor veya çalışma saati bulunamadı."
                });

            return Ok(schedule);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpDelete("{scheduleId:guid}")]
    public async Task<IActionResult> Delete(Guid doctorId, Guid scheduleId, CancellationToken cancellationToken)
    {
        var deleted = await _scheduleService.DeleteAsync(
            doctorId, scheduleId, cancellationToken);

        if (!deleted)
            return NotFound(new
            {
                message = "Çalışma saati bulunamadı."
            });

        return NoContent();
    }
}