using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

//Add advisory contract repository 

namespace CleanArchitecture.Infrastructure.Configurations
{
    public sealed class AdvisoryContractConfiguration : IEntityTypeConfiguration<AdvisoryContract>
    {
        public void Configure(EntityTypeBuilder<AdvisoryContract> builder)
        {
            //builder
            //    .Property(contract => contract.ThesisTopic)
            //    .IsRequired()
            //    .HasMaxLength(255);

            //builder
            //    .Property(contract => contract.Message)
            //    .IsRequired()
            //    .HasMaxLength(1000);
          
            

            //builder
            //    .Property(contract => contract.Status)
            //    .IsRequired()
            //    .HasMaxLength(20);

            //builder
            //    .HasOne(contract => contract.Professor)
            //    .WithMany()
            //    .HasForeignKey(contract => contract.ProfessorId);

            //builder
            //    .HasOne(contract => contract.Student)
            //    .WithMany()
            //    .HasForeignKey(contract => contract.StudentId);

            //builder
            //    .HasOne(contract => contract.ResearchLine)
            //    .WithMany()
            //    .HasForeignKey(contract => contract.ResearchLineId);

            builder.HasData( new AdvisoryContract(Ids.Seed.AdvisoryContractId,Ids.Seed.Professor1Id,Ids.Seed.Student1Id,Ids.Seed.ResearchLine1Id,"Wifi 802.22, de alrgo alcance en zonas rurales", "Me dirijo a usted con el propósito de solicitar sasesoría para mi tesis de grado",1,DateTime.Now),
                new AdvisoryContract(Ids.Seed.AdvisoryContract1Id, Ids.Seed.Professor1Id, Ids.Seed.StudentId, Ids.Seed.ResearchLine1Id, "Sensores Iot y sus aplicaciones en la agricultura", "Me dirijo a usted con el propósito de solicitar sasesoría para mi tesis de grado", 0, DateTime.Now));
        }
    }
}