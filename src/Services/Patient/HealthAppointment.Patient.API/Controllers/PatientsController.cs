using HealthAppointment.Patient.Application.DTOs;
using HealthAppointment.Patient.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthAppointment.Patient.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PatientResponseDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<PatientResponseDto>> Create([FromBody] CreatePatientRequestDto request, CancellationToken cancellationToken)
    {
        var patient = await _patientService.CreateAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = patient.Id },
            patient);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PatientResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<PatientResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var patient = await _patientService.GetByIdAsync(id, cancellationToken);

        if (patient is null)
        {
            return NotFound();
        }

        return Ok(patient);
    }
}
