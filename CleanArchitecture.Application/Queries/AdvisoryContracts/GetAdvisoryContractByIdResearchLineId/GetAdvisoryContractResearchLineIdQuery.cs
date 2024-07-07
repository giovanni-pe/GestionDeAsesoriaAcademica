using System;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using CleanArchitecture.Application.ViewModels.Sorting;
using MediatR;

public sealed record GetAdvisoryContractByResearchLineIdQuery(Guid ResearchLineId, int PageNumber, int PageSize, SortQuery? SortQuery = null, bool IncludeDeleted = false, string? SearchTerm = null) : IRequest<PagedResult<AdvisoryContractViewModel>>;
