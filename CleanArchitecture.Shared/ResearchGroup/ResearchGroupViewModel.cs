using System;

namespace CleanArchitecture.Shared.ResearchGRoup;

public sealed record ResarchGroupViewModel(
    Guid Id,
    string Name,
    string code);