//using CleanArchitecture.Domain.Errors;
//using FluentValidation;

//namespace CleanArchitecture.Domain.Commands.Appointments.DeleteUserCalendar;

//public sealed class DeleteUserCalendarCommandValidation : AbstractValidator<DeleteUserCalendarCommand>
//{
//    public DeleteUserCalendarCommandValidation()
//    {
//        AddRuleForId();
//    }

//    private void AddRuleForId()
//    {
//        RuleFor(cmd => cmd.AggregateId)
//            .NotEmpty()
//            .WithErrorCode(DomainErrorCodes.Appointment.EmptyId)
//            .WithMessage("Appointment id may not be empty");
//    }
//}