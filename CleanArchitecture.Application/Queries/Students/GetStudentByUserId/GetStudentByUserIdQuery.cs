using System;
using CleanArchitecture.Application.ViewModels.Students;
using MediatR;

namespace CleanArchitecture.Application.Queries.Students.GetStudentByUserId
{
    public sealed record GetStudentByUserIdQuery(Guid UserId) : IRequest<StudentViewModel?>;
}
