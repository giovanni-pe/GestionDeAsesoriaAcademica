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
    public string Code { get; set; } = string.Empty;
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static StudentViewModel FromStudent(Student Student)
    {
        return new StudentViewModel
        {
            Id = Student.Id,
            Code = Student.Code,
            UserId = Student.UserId,

           // Users = Student.Users.Select(UserViewModel.FromUser).ToList()
        };
    }
}