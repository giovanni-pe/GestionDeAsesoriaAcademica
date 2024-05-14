using System;

namespace CleanArchitecture.Domain.Commands.ResearchLines.UpdateResearchLine;

public sealed class UpdateResearchLineCommand : CommandBase
{
    private static readonly UpdateResearchLineCommandValidation s_validation = new();

    public string Name { get; }

    public UpdateResearchLineCommand(Guid ResearchLineId, string name) : base(ResearchLineId)
    {
        Name = name;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}