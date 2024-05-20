using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.ResearchLines;

public sealed class ResearchLineViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static ResearchLineViewModel FromResearchLine(ResearchLine ResearchLine)
    {
        return new ResearchLineViewModel
        {
            Id = ResearchLine.Id,
            Name = ResearchLine.Name,
           // Users = ResearchLine.Users.Select(UserViewModel.FromUser).ToList()
        };
    }
}