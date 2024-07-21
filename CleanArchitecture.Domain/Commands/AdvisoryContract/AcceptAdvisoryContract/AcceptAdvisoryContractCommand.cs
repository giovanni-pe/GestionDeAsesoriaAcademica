using System;
using CleanArchitecture.Domain.Commands;
using FluentValidation.Results;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.AcceptAdvisoryContract
{
    public sealed class AcceptAdvisoryContractCommand : CommandBase
    {
        private static readonly AcceptAdvisoryContractCommandValidation s_validation = new();

        public Guid AdvisoryContractId { get; }
        public string AcceptanceMessage { get; }

        public AcceptAdvisoryContractCommand(Guid advisoryContractId, string acceptanceMessage) : base(advisoryContractId)
        {
            AdvisoryContractId = advisoryContractId;
            AcceptanceMessage = acceptanceMessage;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
