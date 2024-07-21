using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.AdvisoryContract;
using MediatR;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.AcceptAdvisoryContract
{
    public sealed class AcceptAdvisoryContractCommandHandler : CommandHandlerBase,
        IRequestHandler<AcceptAdvisoryContractCommand>
    {
        private readonly IAdvisoryContractRepository _advisoryContractRepository;
        private readonly IUser _user;

        public AcceptAdvisoryContractCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IAdvisoryContractRepository advisoryContractRepository,
            IUser user) : base(bus, unitOfWork, notifications)
        {
            _advisoryContractRepository = advisoryContractRepository;
            _user = user;
        }

        public async Task Handle(AcceptAdvisoryContractCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var advisoryContract = await _advisoryContractRepository.GetByIdAsync(request.AdvisoryContractId);

            if (advisoryContract is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no AdvisoryContract with Id {request.AdvisoryContractId}",
                        ErrorCodes.ObjectNotFound));

                return;
            }

            advisoryContract.SetProfessorMessage(request.AcceptanceMessage);
            advisoryContract.SetStatus(1);
            Console.WriteLine(advisoryContract);
            if (await CommitAsync())
            {
                //await Bus.RaiseEventAsync(new AdvisoryContractAcceptedEvent(
                //    advisoryContract.Id,
                //    advisoryContract.ProfessorId,
                //    advisoryContract.StudentId,
                //    advisoryContract.ResearchLineId));
            }
        }
    }
}
