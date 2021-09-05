using Domain.Model.Aggregate;
using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces
{
    public interface IOrderService
    {
        Task<IServiceResult<UserPosition>> BuyShare(string shareSymbol, int amount, string userCpf);
    }
}
