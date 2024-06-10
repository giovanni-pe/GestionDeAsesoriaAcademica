using System;

namespace CleanArchitecture.Application.ViewModels.Professors;

public sealed record UpdateProfessorViewModel(
    Guid Id,
    Guid userId, Guid researchGroupId, bool isCoordinator);