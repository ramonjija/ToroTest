using Domain.Model.Entities;
using Domain.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(InvestmentsDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByCpf(string cpf)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.CPF.Equals(cpf));
        }

        public async Task<User> GetUserByName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.UserName.Equals(userName));
        }
    }
}
