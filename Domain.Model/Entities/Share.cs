using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Entities
{
    public class Share
    {
        public Share()
        {
        }

        public Share(int shareId, string symbol, decimal currentPrice)
        {
            ShareId = shareId;
            Symbol = symbol;
            CurrentPrice = currentPrice;
        }

        public int ShareId { get; set; }
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
