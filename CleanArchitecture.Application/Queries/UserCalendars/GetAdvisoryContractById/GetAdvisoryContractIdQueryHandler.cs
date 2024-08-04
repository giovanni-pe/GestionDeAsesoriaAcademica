using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.UserCalendars;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.UserCalendars.GetUserCalendarById;

public sealed class GetUserCalendarByIdQueryHandler :
    IRequestHandler<GetUserCalendarByIdQuery, UserCalendarViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IUserCalendarRepository _UserCalendarRepository;

    public GetUserCalendarByIdQueryHandler(IUserCalendarRepository UserCalendarRepository, IMediatorHandler bus)
    {
        _UserCalendarRepository = UserCalendarRepository;
        _bus = bus;
    }

    public async Task<UserCalendarViewModel?> Handle(GetUserCalendarByIdQuery request, CancellationToken cancellationToken)
    {
        var UserCalendar = await _UserCalendarRepository.GetByIdAsync(request.UserCalendarId);

        if (UserCalendar is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetUserCalendarByIdQuery),
                    $"UserCalendar with id {request.UserCalendarId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return UserCalendarViewModel.FromUserCalendar(UserCalendar);
    }
}