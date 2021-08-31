using Domain.Model.Entities;
using Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(InvestmentsDbContext context) : base(context)
        {
        }
    }
}
