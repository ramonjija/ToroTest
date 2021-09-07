using DataAccess.Configuration;
using Domain.Model.Aggregate;
using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class InvestmentsDbContext : DbContext
    {
        public InvestmentsDbContext(DbContextOptions<InvestmentsDbContext> options) : base(options)
        {
        }

        protected InvestmentsDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<EntityValidator>();
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new ShareConfiguration().Configure(modelBuilder.Entity<Share>());
            new PositionConfiguration().Configure(modelBuilder.Entity<Position>());
            new UserPositionConfiguration().Configure(modelBuilder.Entity<UserPosition>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<UserPosition> UserPositions { get; set; }


    }
}
