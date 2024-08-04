﻿using CleanArchitecture.Domain.DomainEvents;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Infrastructure.EventSourcing;
using CleanArchitecture.Infrastructure.Integration;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        string migrationsAssemblyName,
        string connectionStringName = "DefaultConnection")
    {
        // Add event store db context
        services.AddDbContext<EventStoreDbContext>(
            options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(connectionStringName),
                    b => b.MigrationsAssembly(migrationsAssemblyName));
            });

        services.AddDbContext<DomainNotificationStoreDbContext>(
            options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(connectionStringName),
                    b => b.MigrationsAssembly(migrationsAssemblyName));
            });

        // Core Infra
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
        services.AddScoped<IEventStoreContext, EventStoreContext>();
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        services.AddScoped<IDomainEventStore, DomainEventStore>();
        services.AddScoped<IMediatorHandler, InMemoryBus>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IResearchGroupRepository, ResearchGroupRepository>();
        services.AddScoped<IResearchLineRepository, ResearchLineRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IProfessorRepository, ProfessorRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IAdvisoryContractRepository, AdvisoryContractRepository>();
        services.AddScoped<ICalendarTokenRepository, CalendarTokenRepository>();
        services.AddScoped<IUserCalendarRepository, UserCalendarRepository>();
        //Integration calendar
        services.AddScoped<IGoogleCalendarIntegration,GoogleCalendarIntegration>();
        return services;
    }
}