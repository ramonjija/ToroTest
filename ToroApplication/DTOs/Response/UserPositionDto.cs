using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToroApplication.DTOs.Response
{
    public class UserPositionDto
    {
        public int UserPositionId { get; set; }
        public double CheckingAccountAmount { get; set; }
        public IEnumerable<PositionDto> Positions { get; set;}
        public double Consolidated { get; set; }
    }
}
