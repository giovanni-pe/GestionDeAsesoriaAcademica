using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Estudiante;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Estudiantes.UpdateEstudiante;

public sealed class UpdateEstudianteCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateEstudianteCommand>
{
    private readonly IEstudianteRepository _EstudianteRepository;
    private readonly IUser _user;

    public UpdateEstudianteCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEstudianteRepository EstudianteRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _EstudianteRepository = EstudianteRepository;
        _user = user;
    }

    public async Task Handle(UpdateEstudianteCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to update Estudiante {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var Estudiante = await _EstudianteRepository.GetByIdAsync(request.AggregateId);

        if (Estudiante is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Estudiante with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        Estudiante.SetFirstName(request.FirstName);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new EstudianteUpdatedEvent(
                Estudiante.Id,
                Estudiante.FirstName));
        }
    }
}