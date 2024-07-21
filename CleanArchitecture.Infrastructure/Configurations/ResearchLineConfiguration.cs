using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class ResearchLineConfiguration : IEntityTypeConfiguration<ResearchLine>
{
    public void Configure(EntityTypeBuilder<ResearchLine> builder)
    {
        builder
            .Property(ResearchLine=> ResearchLine.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.ResearchLine.Name);

        builder.HasData(new ResearchLine(
          Ids.Seed.ResearchLineId,
          "Arquitectura de Software",Ids.Seed.ResearchGroupId,"ASW123"),
          new ResearchLine(
          Ids.Seed.ResearchLine1Id,
          "Internet de las Cosas", Ids.Seed.ResearchGroup1Id, "IoT"));
    }
}