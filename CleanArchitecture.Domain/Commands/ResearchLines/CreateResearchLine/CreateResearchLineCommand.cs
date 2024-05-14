using System;

namespace CleanArchitecture.Domain.Commands.ResearchLines.CreateResearchLine;

public sealed class CreateResearchLineCommand : CommandBase
{
    private static readonly CreateResearchLineCommandValidation s_validation = new();

    public string Name { get; }
    public string Code { get; }

    public CreateResearchLineCommand(Guid ResearchLineId, string name,string code) : base(ResearchLineId)
    {
        Name = name;
        Code = code;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}