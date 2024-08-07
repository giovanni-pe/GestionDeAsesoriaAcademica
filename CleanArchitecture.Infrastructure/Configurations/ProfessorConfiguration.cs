using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public sealed class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder
                .Property(professor => professor.IsCoordinator)
                .IsRequired();

            /* builder
                 .HasOne(professor => professor.User)
                 .WithMany()
                 .HasForeignKey(professor => professor.UserId);

             builder
                 .HasOne(professor => professor.ResearchGroup)
                 .WithMany()
                 .HasForeignKey(professor => professor.ResearchGroupId);*/
            builder.HasData( new Professor(Ids.Seed.ProfessorId,Ids.Seed.UserIbarraId,Ids.Seed.ResearchGroupId,false) ,
                new Professor(Ids.Seed.Professor1Id, Ids.Seed.UserGardynId, Ids.Seed.ResearchGroup1Id, false));


        }
    }
}