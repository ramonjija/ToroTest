using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Aggregate
{
    public class UserPosition
    {
        public UserPosition()
        {

        }
        public UserPosition(int userPositionId, IEnumerable<Position> positions, double checkingAccountAmount, double consolidated, User user)
        {
            UserPositionId = userPositionId;
            Positions = positions;
            CheckingAccountAmount = checkingAccountAmount;
            Consolidated = consolidated;
            User = user;
        }

        public int UserPositionId { get; set; }
        public IEnumerable<Position> Positions { get; set; }
        public double CheckingAccountAmount { get; set; }
        public double Consolidated { get; set; }
        public User User { get; set; }
    }
}
