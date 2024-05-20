using System;

namespace CleanArchitecture.Shared.Estudiante;

public sealed record EstudianteViewModel(
    Guid Id,
    string FirstName,
    string LastName);