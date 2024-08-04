//using System;

//namespace CleanArchitecture.Domain.Commands.Appointments.DeleteUserCalendar;

//public sealed class DeleteUserCalendarCommand : CommandBase
//{
//    private static readonly DeleteUserCalendarCommandValidation s_validation = new();

//    public DeleteUserCalendarCommand(Guid AppointmentId) : base(AppointmentId)
//    {
//    }

//    public override bool IsValid()
//    {
//        ValidationResult = s_validation.Validate(this);
//        return ValidationResult.IsValid;
//    }
//}