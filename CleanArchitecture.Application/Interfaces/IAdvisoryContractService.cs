using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Interfaces;

public interface IAdvisoryContractService
{
    public Task<Guid> CreateAdvisoryContractAsync(CreateAdvisoryContractViewModel AdvisoryContract);
    public Task UpdateAdvisoryContractAsync(UpdateAdvisoryContractViewModel AdvisoryContract);
    public Task DeleteAdvisoryContractAsync(Guid AdvisoryContractId);
    public Task<AdvisoryContractViewModel?> GetAdvisoryContractByIdAsync(Guid AdvisoryContractId);
    Task<PagedResult<AdvisoryContractViewModel>> GetAdvisoryContractsByResearchLineIdAsync(Guid researchLineId, int pageNumber, int pageSize, SortQuery? sortQuery = null, bool includeDeleted = false, string? searchTerm = null);
    Task<PagedResult<AdvisoryContractViewModel>> GetAdvisoryContractsByProfessorIdAsync(Guid professorId, int pageNumber, int pageSize, SortQuery? sortQuery = null, bool includeDeleted = false, string? searchTerm = null);

    public Task<PagedResult<AdvisoryContractViewModel>> GetAllAdvisoryContractsAsync(Guid researchLineId,DateTime startDate,DateTime endDate,
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);

    public Task AcceptAdvisoryContractAsync(Guid advisoryContractId, string acceptanceMessage);
}