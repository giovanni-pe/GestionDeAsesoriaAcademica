using System;

namespace CleanArchitecture.Application.ViewModels.Professors;

public sealed record CreateProfessorViewModel(Guid userId,Guid researchGroupId,bool isCoordinator);