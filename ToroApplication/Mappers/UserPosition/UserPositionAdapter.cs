using Domain.Model.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToroApplication.DTOs.Response;

namespace ToroApplication.Mappers
{
    public class UserPositionAdapter
    {
        public UserPositionDto Adapt(UserPosition userPosition)
        {
            return new UserPositionDto()
            {
                CheckingAccountAmount = userPosition.CheckingAccountAmount,
                Consolidated = userPosition.Consolidated,
                Positions = userPosition.Positions?.Select(c => new PositionDto()
                {
                    Amount = c.Amout,
                    CurrentPrice = c.Share.CurrentPrice,
                    Symbol = c.Share.Symbol
                }).ToList()
            };
        }
    }
}
