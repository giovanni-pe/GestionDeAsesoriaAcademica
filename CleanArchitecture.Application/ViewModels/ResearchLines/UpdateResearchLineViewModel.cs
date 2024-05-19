using System;

namespace CleanArchitecture.Application.ViewModels.ResearchLines;

public sealed record UpdateResearchLineViewModel(
    Guid Id,
    string Name,String Code);