using System;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.IntegrationTests.Fixtures;

public sealed class ProfessorTestFixture : TestFixtureBase
{
    
    public Guid CreatedProfessorId { get;} = Guid.NewGuid();

    protected override void SeedTestData(ApplicationDbContext context)
    {
       base.SeedTestData(context);
        context.Users.Add(new User(Guid.NewGuid(), Ids.Seed.TenantId, "test@user.de", "test", "user", "Test User", UserRole.User));
        context.ResearchGroups.Add(new ResearchGroup(Ids.Seed.ResearchGroupId, "test", "test"));
        

        context.Professors.Add(new Professor(
            CreatedProfessorId, Ids.Seed.UserId, Ids.Seed.ResearchGroupId, false));
       // context.Tenants.Add(new Tenant(
       //    Ids.Seed.TenantId,
       //    "Test Tenant"));
       // context.Users.Add(new User(
       //  Guid.NewGuid(),
       // Ids.Seed.TenantId,
       //"test@user.de",
       //"test",
       // "user",
       // "Test User",
       // UserRole.User));

        context.SaveChanges();
    }
}