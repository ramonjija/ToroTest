using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces
{
    public interface IShareRepository : IBaseRepository<Share>
    {
        Task<Share> GetBySymbol(string shareSymbol);
    }
}
