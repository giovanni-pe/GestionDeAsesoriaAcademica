using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.AdvisoryContracts;

public sealed class AdvisoryContractViewModel
{
    public Guid AdvisoryContractId { get; set; }
    public Guid ProfessorId { get; private set; }

    public Guid StudentId { get; private set; }

    public Guid ResearchLineId { get; private set; }
    public string ThesisTopic { get; private set; }
    public string Message { get; private set; }
    public string Status { get; private set; }
    public StudentViewModel Student { get; set; }
    public ProfessorViewModel Professor { get; private set; }
    public ResearchLineViewModel ResearchLine { get; private set; }


    public static AdvisoryContractViewModel FromAdvisoryContract(AdvisoryContract AdvisoryContract)
    {
        return new AdvisoryContractViewModel
        {
           AdvisoryContractId = AdvisoryContract.Id,
           ProfessorId = AdvisoryContract.ProfessorId,
           StudentId = AdvisoryContract.StudentId,
           ResearchLineId = AdvisoryContract.ResearchLineId,
           ThesisTopic = AdvisoryContract.ThesisTopic,
           Message = AdvisoryContract.Message,
            Student = StudentViewModel.FromStudent(AdvisoryContract.Student),
            Professor=ProfessorViewModel.FromProfessor(AdvisoryContract.Professor),
            ResearchLine=ResearchLineViewModel.FromResearchLine(AdvisoryContract.ResearchLine),
        };
    }
}