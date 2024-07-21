using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .Property(student => student.Code)
                .IsRequired();

       builder.HasData(new Student(
       Ids.Seed.StudentId, Ids.Seed.UserGiovanniId,"0020210008"),
           new Student(
       Ids.Seed.Student1Id, Ids.Seed.UserLuzId, "0020210008"));

            
        }
    }
}
