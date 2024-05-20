using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Estudiante;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Estudiantes.DeleteEstudiante;

public sealed class DeleteEstudianteCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteEstudianteCommand>
{
    private readonly IEstudianteRepository _EstudianteRepository;
   // private readonly IReserchLine _user;
    private readonly IUserRepository _userRepository;

    public DeleteEstudianteCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEstudianteRepository EstudianteRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _EstudianteRepository = EstudianteRepository;
      //  _userRepository = userRepository;
       // _user = user;
    }

    public async Task Handle(DeleteEstudianteCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }
/*
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete ResearchGroup {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }*/

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
        /*
                var ResearchGroupUsers = _userRepository
                    .GetAll()
                    .Where(x => x.ResearchGroupId == request.AggregateId);

                _userRepository.RemoveRange(ResearchGroupUsers);*/

        _EstudianteRepository.Remove(Estudiante);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new EstudianteDeletedEvent(Estudiante.Id));
        }
    }
}