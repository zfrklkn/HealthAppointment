using HealthAppointment.Appointment.Application.DTOs.Appointments;
using HealthAppointment.Appointment.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthAppointment.Appointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentRequestDto request, CancellationToken cancellationToken)
        {
            var correlationId = _appointmentService.CreateAsync(request, cancellationToken);

            return Accepted(new
            {
                CorrelationId = correlationId,
                Status = "Processing",
                Message = "Randevu oluşturma süreci başlatıldı."
            });
        }
    }
}
