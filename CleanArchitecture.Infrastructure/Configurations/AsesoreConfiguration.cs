using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class AsesoreConfiguration : IEntityTypeConfiguration<Asesore>
{
    public void Configure(EntityTypeBuilder<Asesore> builder)
    {
        builder
            .Property(Asesore => Asesore.Nombre)
            .IsRequired()
            .HasMaxLength(MaxLengths.Asesore.Nombre);

        builder.HasData(new Asesore(
            Ids.Seed.AsesoreId,
            "marcos","canales"));
    }

}