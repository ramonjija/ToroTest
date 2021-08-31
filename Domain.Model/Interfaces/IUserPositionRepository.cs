using Domain.Model.Aggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces
{
    public interface IUserPositionRepository : IBaseRepository<UserPosition>
    {
        Task<UserPosition> GetPositionByCpf(string cpf);
    }
}
