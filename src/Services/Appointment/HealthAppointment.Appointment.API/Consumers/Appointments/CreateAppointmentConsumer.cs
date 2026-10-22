using HealthAppointment.Appointment.Application.Interfaces;
using HealthAppointment.Contracts.Appointments;
using AppointmentEntity = HealthAppointment.Appointment.Domain.Entities.Appointment;
using MassTransit;

namespace HealthAppointment.Appointment.API.Consumers.Appointments
{
    public sealed class CreateAppointmentConsumer : IConsumer<CreateAppointment>
    {
        private readonly IAppointmentRepository _appointmentsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppointmentConsumer(IAppointmentRepository appointmentsRepository, IUnitOfWork unitOfWork)
        {
            _appointmentsRepository = appointmentsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<CreateAppointment> context)
        {
            var message = context.Message;

            try
            {
                if (message.PatientId == Guid.Empty || message.DoctorId == Guid.Empty || message.SpecialtyId == Guid.Empty || message.EndTime <= message.StartTime)
                {
                    await context.Publish(new AppointmentCreationFailed(
                        message.CorrelationId,
                        "Randevu bilgileri geçersiz."));

                    return;
                }

                var existingAppointment = await _appointmentsRepository.GetByCreationCorrelationIdAsync(message.CorrelationId, context.CancellationToken);

                if (existingAppointment is not null)
                {
                    await context.Publish(new AppointmentCreated(
                            message.CorrelationId,
                            existingAppointment.Id),
                            context.CancellationToken);

                    return;
                }

                await _unitOfWork.BeginTransactionAsync(context.CancellationToken);

                var appointment = new AppointmentEntity(
                    message.CorrelationId,
                    message.PatientId,
                    message.DoctorId,
                    message.SpecialtyId,
                    message.StartTime,
                    message.EndTime,
                    message.Reason
                    );

                await _appointmentsRepository.AddAsync(appointment, context.CancellationToken);

                await _unitOfWork.CommitTransactionAsync(context.CancellationToken);

                await context.Publish(new AppointmentCreated(
                    message.CorrelationId,
                    appointment.Id));

            }
            catch (Exception)
            {

                await _unitOfWork.RollbackTransactionAsync(CancellationToken.None);

                throw;
            }
        }
    }
}
