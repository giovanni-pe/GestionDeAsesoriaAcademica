using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class EstudianteConfiguration : IEntityTypeConfiguration<Estudiante>
{
    public void Configure(EntityTypeBuilder<Estudiante> builder)
    {
        builder
            .Property(Estudiante => Estudiante.FirstName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Estudiante.FirstName);

        builder.HasData(new Estudiante(
            Ids.Seed.EstudianteId,
            "hubel","solis"));
    }

}