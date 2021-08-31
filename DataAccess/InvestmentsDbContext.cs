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

        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<UserPosition> UserPositions { get; set; }


    }
}
