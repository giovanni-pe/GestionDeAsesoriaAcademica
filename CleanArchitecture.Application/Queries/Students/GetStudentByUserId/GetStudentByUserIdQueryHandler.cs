using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Students.GetStudentByUserId
{
    public sealed class GetStudentByUserIdQueryHandler : IRequestHandler<GetStudentByUserIdQuery, StudentViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IStudentRepository _studentRepository;

        public GetStudentByUserIdQueryHandler(IStudentRepository studentRepository, IMediatorHandler bus)
        {
            _studentRepository = studentRepository;
            _bus = bus;
        }

        public async Task<StudentViewModel?> Handle(GetStudentByUserIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByUserIdAsync(request.UserId);

            if (student is null)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        nameof(GetStudentByUserIdQuery),
                        $"Student with user id {request.UserId} could not be found",
                        ErrorCodes.ObjectNotFound));
                return null;
            }

            return StudentViewModel.FromStudent(student);
        }
    }
}
