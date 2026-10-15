using HealthAppointment.Contracts.Appointments;
using HealthAppointment.Patient.Application.Interfaces.Services;
using MassTransit;

namespace HealthAppointment.Patient.API.Consumers.Appointments;

public sealed class ValidatePatientConsumer : IConsumer<ValidatePatient>
{
    private readonly IPatientService _patientService;

    public ValidatePatientConsumer(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public async Task Consume(ConsumeContext<ValidatePatient> context)
    {
        var patientId = context.Message.PatientId;

        var CanMakeAppointment = await _patientService.CanMakeAppointmentAsync(patientId);

        if (CanMakeAppointment)
        {
            await context.Publish(new PatientValidated(context.Message.CorrelationId, patientId));

            return;
        }

        await context.Publish(new PatientValidationFailed(context.Message.CorrelationId, "Hasta bulumadı veya randevu almaya uygun değil"));
    }
}
