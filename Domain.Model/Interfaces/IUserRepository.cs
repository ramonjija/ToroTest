using Domain.Model.Entities;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByName(string userName);
        Task<User> GetUserByCpf(string cpf);

    }
}
