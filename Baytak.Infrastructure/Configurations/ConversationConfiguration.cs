using Baytak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Infrastructure.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasOne(c => c.User)
                   .WithMany()
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Agent)
                   .WithMany()
                   .HasForeignKey(c => c.AgentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
