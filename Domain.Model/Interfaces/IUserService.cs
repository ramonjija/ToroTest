using Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces
{
    public interface IUserService
    {
        public Task<IServiceResult<User>> CreateUser(string name, string cpf, string passwordHash);
        public Task<IServiceResult<User>> GetUser(int userId);
        public Task<IServiceResult<User>> GetUser(string cpf, string password);
        public Task<IServiceResult<IEnumerable<User>>> GetUser();
        public Task<IServiceResult<User>> UpdateUser(int userId, string userName, string password, int TypeId);
        public Task<IServiceResult<User>> DeleteUser(int userId);
    }
}
