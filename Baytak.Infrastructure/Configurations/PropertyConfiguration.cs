using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Baytak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Infrastructure.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Price)
               .HasColumnType("decimal(18,2)");

        builder.HasOne(p => p.Agent)
               .WithMany(u => u.Properties)
               .HasForeignKey(p => p.AgentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
