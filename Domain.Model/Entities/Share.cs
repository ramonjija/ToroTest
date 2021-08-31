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

        public Share(int shareId, string symbol, double currentPrice)
        {
            ShareId = shareId;
            Symbol = symbol;
            CurrentPrice = currentPrice;
        }

        public int ShareId { get; set; }
        public string Symbol { get; set; }
        public double CurrentPrice { get; set; }
    }
}
