using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Entities
{
    public class Position
    {
        public Position()
        {
        }

        public Position(int positionId, Share share, int amout)
        {
            PositionId = positionId;
            Share = share;
            Amout = amout;
        }
        public Position(Share share, int amout)
        {
            Share = share;
            Amout = amout;
        }

        public int PositionId { get; set; }
        public Share Share { get; set; }
        public int Amout { get; set; }
    }
}
