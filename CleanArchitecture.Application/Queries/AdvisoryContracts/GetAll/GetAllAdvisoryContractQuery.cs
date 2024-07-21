using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using MediatR;
using System;

namespace CleanArchitecture.Application.Queries.AdvisoryContracts.GetAll;

public sealed record AdvisoryContractsQuery(Guid researchLineId,DateTime startDate,DateTime endDate,
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<AdvisoryContractViewModel>>;