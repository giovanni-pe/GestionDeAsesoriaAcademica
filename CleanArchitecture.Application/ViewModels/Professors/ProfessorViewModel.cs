using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.Professors;

public sealed class ProfessorViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ResearchGroupId { get; set; }
    public bool IsCoordinator { get; set; } 
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static ProfessorViewModel FromProfessor(Professor Professor)
    {
        return new ProfessorViewModel
        {
            Id = Professor.Id,
            UserId = Professor.UserId,
            ResearchGroupId = Professor.ResearchGroupId,
            IsCoordinator = Professor.IsCoordinator,
            

           // Users = Professor.Users.Select(UserViewModel.FromUser).ToList()
        };
    }
}