using Domain.Model.Entities;
using Domain.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ShareRepository : BaseRepository<Share>, IShareRepository
    {
        public ShareRepository(InvestmentsDbContext context) : base(context)
        {
        }

        public async Task<Share> GetBySymbol(string shareSymbol)
        {
            return await _context.Shares.FirstOrDefaultAsync(c => c.Symbol.Equals(shareSymbol)).ConfigureAwait(false);
        }
    }
}
