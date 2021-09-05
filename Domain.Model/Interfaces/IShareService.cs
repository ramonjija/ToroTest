using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces
{
    public interface IShareService
    {
        Task<IServiceResult<IEnumerable<Share>>> GetAllShares();
    }
}
