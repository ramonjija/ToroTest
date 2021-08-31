using Domain.Model.Aggregate;
using Domain.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserPositionRepository : BaseRepository<UserPosition>, IUserPositionRepository
    {
        public UserPositionRepository(InvestmentsDbContext context) : base(context)
        {
        }

        public async Task<UserPosition> GetPositionByCpf(string cpf)
        {
            return await _context
                .UserPositions
                .Include(c => c.Positions)
                .ThenInclude(c => c.Share)
                .FirstOrDefaultAsync(c => c.User.CPF.Equals(cpf)).ConfigureAwait(false);
        }
    }
}
