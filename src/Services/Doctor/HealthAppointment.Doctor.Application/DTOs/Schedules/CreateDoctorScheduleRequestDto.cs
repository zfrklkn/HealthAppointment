namespace HealthAppointment.Doctor.Application.DTOs.Schedules;

public class CreateDoctorScheduleRequestDto
{
    public DayOfWeek DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}
