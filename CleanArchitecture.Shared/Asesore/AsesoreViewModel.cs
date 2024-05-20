using System;

namespace CleanArchitecture.Shared.Asesore;

public sealed record AsesoreViewModel(
    Guid Id,
    string Nombre,
    string Apellido);