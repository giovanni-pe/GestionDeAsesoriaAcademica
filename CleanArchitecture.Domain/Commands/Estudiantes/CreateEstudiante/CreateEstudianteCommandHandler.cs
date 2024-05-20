using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Estudiante;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Estudiantes.CreateEstudiante;

public sealed class CreateEstudianteCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateEstudianteCommand>
{
    private readonly IEstudianteRepository _EstudianteRepository;
    private readonly IUser _user;

    public CreateEstudianteCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEstudianteRepository EstudianteRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _EstudianteRepository = EstudianteRepository;
        _user = user;
    }

    public async Task Handle(CreateEstudianteCommand request, CancellationToken cancellationToken)
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
                    $"No permission to create ResearchGroup {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        if (await _EstudianteRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a Estudiante with Id {request.AggregateId}",
                    DomainErrorCodes.Estudiante.AlreadyExists));

            return;
        }

        var Estudiante = new Estudiante(
            request.AggregateId,
            request.FirstName,request.LastName);

        _EstudianteRepository.Add(Estudiante);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new EstudianteCreatedEvent(
                Estudiante.Id,
                Estudiante.FirstName));
        }
    }
}