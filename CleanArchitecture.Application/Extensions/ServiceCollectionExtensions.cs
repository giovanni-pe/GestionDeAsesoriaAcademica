using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Tenants.GetAll;
using CleanArchitecture.Application.Queries.Tenants.GetTenantById;
using CleanArchitecture.Application.Queries.ResearchGroups.GetAll;
using CleanArchitecture.Application.Queries.ResearchGroups.GetResearchGroupById;
using CleanArchitecture.Application.Queries.Users.GetAll;
using CleanArchitecture.Application.Queries.Users.GetUserById;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Tenants;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IResearchGroupService, ResearchGroupService>();
        return services;
    }

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        // User
        services.AddScoped<IRequestHandler<GetUserByIdQuery, UserViewModel?>, GetUserByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, PagedResult<UserViewModel>>, GetAllUsersQueryHandler>();

        // Tenant
        services.AddScoped<IRequestHandler<GetTenantByIdQuery, TenantViewModel?>, GetTenantByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<TenantsQuery, PagedResult<TenantViewModel>>, GetAllTenantsQueryHandler>();
        // ResearchGroup
        services.AddScoped<IRequestHandler<GetResearchGroupByIdQuery, ResearchGroupViewModel?>, GetResearchGroupByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<ResearchGroupsQuery, PagedResult<ResearchGroupViewModel>>, GetAllResearchGroupsQueryHandler>();
        return services;
    }

    public static IServiceCollection AddSortProviders(this IServiceCollection services)
    {
        services.AddScoped<ISortingExpressionProvider<TenantViewModel, Tenant>, TenantViewModelSortProvider>();
         services.AddScoped<ISortingExpressionProvider<ResearchGroupViewModel, ResearchGroup>, ResearchGroupViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<UserViewModel, User>, UserViewModelSortProvider>();

        return services;
    }
}