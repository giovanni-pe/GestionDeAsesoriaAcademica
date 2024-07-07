using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.AdvisoryContracts.GetAdvisoryContractByResearchLineId;

public sealed class GetAdvisoryContractByResearchLineIdQueryHandler :
    IRequestHandler<GetAdvisoryContractByResearchLineIdQuery, PagedResult<AdvisoryContractViewModel>>
{
    private readonly IAdvisoryContractRepository _AdvisoryContractRepository;
    private readonly ISortingExpressionProvider<AdvisoryContractViewModel, AdvisoryContract> _sortingExpressionProvider;

    public GetAdvisoryContractByResearchLineIdQueryHandler(
        IAdvisoryContractRepository AdvisoryContractRepository,
        ISortingExpressionProvider<AdvisoryContractViewModel, AdvisoryContract> sortingExpressionProvider)
    {
        _AdvisoryContractRepository = AdvisoryContractRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<AdvisoryContractViewModel>> Handle(
        GetAdvisoryContractByResearchLineIdQuery request,
        CancellationToken cancellationToken)
    {
        var advisoryContractsQuery = _AdvisoryContractRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Include(x => x.Student)
            .Where(x => x.ResearchLineId == request.ResearchLineId && (request.IncludeDeleted || !x.Deleted));

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            // Aquí puedes añadir la lógica de búsqueda si es necesario
        }

        var totalCount = await advisoryContractsQuery.CountAsync(cancellationToken);

        advisoryContractsQuery = advisoryContractsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var advisoryContracts = await advisoryContractsQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(advisoryContract => AdvisoryContractViewModel.FromAdvisoryContract(advisoryContract))
            .ToListAsync(cancellationToken);

        return new PagedResult<AdvisoryContractViewModel>(
            totalCount, advisoryContracts, request.PageNumber, request.PageSize);
    }
}
