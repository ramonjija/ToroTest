using Domain.Model.Aggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces
{
    public interface IUserPositionService
    {
        Task<IServiceResult<UserPosition>> GetUserPosition(string cpf);
    }
}
