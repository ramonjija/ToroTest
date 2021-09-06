using Domain.Model.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration
{
    public class UserPositionConfiguration : IEntityTypeConfiguration<UserPosition>
    {
        public void Configure(EntityTypeBuilder<UserPosition> builder)
        {
            builder.HasKey(c => c.UserPositionId);
            builder.Property(c => c.CheckingAccountAmount)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2);
            builder.Property(c => c.Consolidated)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2);
            builder.HasMany(c => c.Positions);
            builder.HasOne(c => c.User);
            builder.Ignore(c => c.Validator);
        }
    }
}
