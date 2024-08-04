using CleanArchitecture.Domain.Commands.Calendars.CreateCalendar;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.UserCalendars.CreateUserCalendar;

public sealed class CreateUserCalendarCommandValidation : AbstractValidator<CreateCalendarCommand>
{
    public CreateUserCalendarCommandValidation()
    {
    //    AddRuleForId();
        
    }

    //private void AddRuleForId()
    //{
    //    RuleFor(cmd => cmd.AggregateId)
    //        .NotEmpty()
    //        .WithErrorCode(DomainErrorCodes.ResearchGroup.EmptyId)
    //        .WithMessage("ResearchGroup id may not be empty");
    //}


}