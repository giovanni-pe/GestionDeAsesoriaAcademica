using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Calendars.CreateCalendar;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Calendars.CreateCalendar
{
    public sealed class CreateCalendarCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateCalendarCommand>
    {
        private readonly IGoogleCalendarIntegration _googleCalendarIntegration;
        private readonly IUserCalendarRepository _userCalendarRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICalendarTokenRepository _calendarTokenRepository;

        public CreateCalendarCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IGoogleCalendarIntegration googleCalendarIntegration,
            IUserCalendarRepository userCalendarRepository,
            IUserRepository userRepository,
            ICalendarTokenRepository calendarTokenRepository)
            : base(bus, unitOfWork, notifications)
        {
            _googleCalendarIntegration = googleCalendarIntegration;
            _userCalendarRepository = userCalendarRepository;
            _userRepository = userRepository;
            _calendarTokenRepository = calendarTokenRepository;
        }

        public async Task Handle(CreateCalendarCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                await NotifyAsync(new DomainNotification(request.MessageType, "User not found", ErrorCodes.UserNotFound));
               
            }

            try
            {
                // Authorize user and get token
                var credential = await _googleCalendarIntegration.AuthorizeUserAsync(user.Email);
                var calendarToken = new CalendarToken(
                    Guid.NewGuid(),
                    user.Id,
                    credential.Token.AccessToken,
                    credential.Token.RefreshToken,
                    credential.Token.IssuedUtc.AddSeconds(credential.Token.ExpiresInSeconds ?? 3600),
                    user.Email);
                await _calendarTokenRepository.AddOrUpdateTokenAsync(calendarToken);

                // Create calendar
                var calendarId = await _googleCalendarIntegration.CreateCalendarAsync(request.CalendarName, request.TimeZone);
                var iframeUrl = GenerateIframeUrl(calendarId, request.TimeZone);
                var userCalendar = new UserCalendar(Guid.NewGuid(), request.UserId, request.CalendarName, request.TimeZone, calendarId, iframeUrl, request.Description);

                await _userCalendarRepository.AddCalendarAsync(userCalendar);

                if (await CommitAsync())
                {
                    // Optional: Raise event if needed
                }

                
            }
            catch (Exception ex)
            {
                await NotifyAsync(new DomainNotification(request.MessageType, ex.Message,Errors.ErrorCodes.CommitFailed));
               
            }
        }

        private string GenerateIframeUrl(string calendarId, string timeZone)
        {
            return $"https://calendar.google.com/calendar/embed?src={calendarId}&ctz={timeZone}";
        }
    }
}
