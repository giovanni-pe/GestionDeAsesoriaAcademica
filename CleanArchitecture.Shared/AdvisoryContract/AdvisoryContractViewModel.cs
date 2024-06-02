using System;

namespace CleanArchitecture.Shared.Students;

public sealed record AdvisoryContractViewModel(
    Guid Id,
    Guid UserId,
    string Code,
    bool IsDeleted);