using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.Students;

public sealed class StudentViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserViewModel User { get; set; }
    public string Code { get; set; } = string.Empty;
    

    public static StudentViewModel FromStudent(Student student)
    {
        return new StudentViewModel
        {
            Id = student.Id,
            Code = student.Code,
            UserId = student.UserId,
            User = UserViewModel.FromUser(student.User)
        };
    }
}