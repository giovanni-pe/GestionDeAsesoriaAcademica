//using CleanArchitecture.Domain.Errors;
//using FluentValidation;

//namespace CleanArchitecture.Domain.Commands.Appointments.UpdateUserCalendar
//{
//    public sealed class UpdateUserCalendarCommandValidation : AbstractValidator<UpdateUserCalendarCommand>
//    {
//        public UpdateUserCalendarCommandValidation()
//        {
//            AddRuleForId();
//        }

//        private void AddRuleForId()
//        {
//            RuleFor(cmd => cmd.AggregateId)
//                .NotEmpty()
//                .WithErrorCode(DomainErrorCodes.Appointment.EmptyId)
//                .WithMessage("Appointment id may not be empty");
//        }
//    }
//}
