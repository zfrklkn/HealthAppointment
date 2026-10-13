using FluentValidation;
using HealthAppointment.Doctor.Application.DTOs;
using HealthAppointment.Doctor.Application.Interfaces;
using HealthAppointment.Doctor.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthAppointment.Doctor.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IValidator<CreateDoctorRequestDto> _createDoctorValidator;
    private readonly IValidator<UpdateDoctorRequestDto> _updateDoctorValidator;

    public DoctorsController(IDoctorService doctorService, IValidator<CreateDoctorRequestDto> createDoctorValidator, IValidator<UpdateDoctorRequestDto> updateDoctorValidator)
    {
        _doctorService = doctorService;
        _createDoctorValidator = createDoctorValidator;
        _updateDoctorValidator = updateDoctorValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var doctors = await _doctorService.GetAllAsync(cancellationToken);
        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await _doctorService.GetByIdAsync(id, cancellationToken);

        if (doctor is null)
        {
            return NotFound();
        }

        return Ok(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDoctorRequestDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _createDoctorValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var Errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            return BadRequest(new
            {
                message = "Gönderilen bilgiler geçersiz.",
                errors = Errors
            });
        }

        var doctor = await _doctorService.CreateAsync(
            request,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = doctor.Id },
            doctor);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDoctorRequestDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _updateDoctorValidator.ValidateAsync(
            request,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray());

            return BadRequest(new
            {
                message = "Gönderilen bilgiler geçersiz.",
                errors
            });
        }

        var doctor = await _doctorService.UpdateAsync(
            id,
            request,
            cancellationToken);

        if (doctor is null)
        {
            return NotFound();
        }

        return Ok(doctor);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await _doctorService.DeactivateAsync(id, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        var result = await _doctorService.ActivateAsync(id, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
