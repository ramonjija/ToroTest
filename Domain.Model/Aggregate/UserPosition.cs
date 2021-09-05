using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model.Aggregate
{
    public class UserPosition
    {
        public UserPosition()
        {
            Positions = new List<Position>();
        }

        public UserPosition(IEnumerable<Position> positions, double checkingAccountAmount, double consolidated, User user)
        {
            AddPositionsToUser(positions);
            CheckingAccountAmount = checkingAccountAmount;
            Consolidated = consolidated;
            User = user;
        }

        public UserPosition(IEnumerable<Position> positions, double checkingAccountAmount, User user)
        {
            User = user;
            AddPositionsToUser(positions);
            CheckingAccountAmount = checkingAccountAmount;
            Consolidated = ConsolidatePositions();
        }

        public int UserPositionId { get; private set; }
        public List<Position> Positions { get; private set; }
        public double CheckingAccountAmount { get; private set; }
        public double Consolidated { get; private set; }
        public User User { get; private set; }

        private double ConsolidatePositions()
        {
            return CheckingAccountAmount + Positions.Select(c => c.Amout * c.Share.CurrentPrice).Sum();
        }

        public void AddPositionsToUser(IEnumerable<Position> positionsToAdd)
        {
            if (Positions == null)
                Positions = new List<Position>();

            if (positionsToAdd != null)
                Positions.AddRange(positionsToAdd);

            Consolidated = ConsolidatePositions();
        }


        public void RemovePositionsOfUser(IEnumerable<Position> positionsToRemove)
        {
            if (Positions == null)
                Positions = new List<Position>();

            if (positionsToRemove != null)
            {
                HashSet<int> positionIds = new HashSet<int>(positionsToRemove.Select(x => x.PositionId));
                Positions.RemoveAll(c => positionIds.Contains(c.PositionId));
            }
            Consolidated = ConsolidatePositions();
        }

        public UserPosition AddPositionToUser(Share share, int amount)
        {
            var shareValue = (share.CurrentPrice * amount);
            if (CheckingAccountAmount >= shareValue)
            {
                CheckingAccountAmount -= shareValue;
                var existingPosition = Positions.FirstOrDefault(c => c.Share.Symbol == share.Symbol);
                if (existingPosition != null)
                {
                    existingPosition.Amout += amount;
                }
                else
                {
                    Positions.Add(new Position(share, amount));
                }
                Consolidated = ConsolidatePositions();
                return this;
            }
            return null;
        }


        public UserPosition AddBalance(double addedBalance)
        {
            CheckingAccountAmount += addedBalance;
            Consolidated = ConsolidatePositions();
            return this;
        }

    }
}
