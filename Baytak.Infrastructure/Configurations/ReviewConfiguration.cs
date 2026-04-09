using Baytak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Infrastructure.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Agent)
                   .WithMany()
                   .HasForeignKey(r => r.AgentId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
