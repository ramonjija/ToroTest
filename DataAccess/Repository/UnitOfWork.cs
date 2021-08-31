using Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnityOfWork : IUnitOfWork
    {
        private InvestmentsDbContext _dbcontext;
        private UserRepository _users;
        private PositionRepository _positions;
        private UserPositionRepository _userPositions;
        private ShareRepository _shares;


        public UnityOfWork(InvestmentsDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public IUserRepository Users => _users ??= new UserRepository(_dbcontext);

        public IUserPositionRepository UserPositions => _userPositions ??= new UserPositionRepository(_dbcontext);

        public IPositionRepository PositionRepository => _positions ??= new PositionRepository(_dbcontext);

        public IShareRepository ShareRepository => _shares ??= new ShareRepository(_dbcontext);

        public async Task Commit()
        {
            await _dbcontext.SaveChangesAsync();
        }
    }
}
