using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToroApplication.DTOs.Response
{
    public class PositionDto
    {
        public string Symbol { get; set; }
        public int Amount { get; set; }
        public decimal CurrentPrice { get; set; }

    }
}
