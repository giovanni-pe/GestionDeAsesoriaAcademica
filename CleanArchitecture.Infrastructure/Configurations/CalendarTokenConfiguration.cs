using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace CleanArchitecture.Infrastructure.Configurations
{
    public sealed class CalendarTokenConfiguration : IEntityTypeConfiguration<CalendarToken>
    {
        public void Configure(EntityTypeBuilder<CalendarToken> builder)
        {   

        }
    }
}