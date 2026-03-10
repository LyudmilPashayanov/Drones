using System;

namespace World
{
    public struct WorldCoordinates
    {
        public int Row, Col, Depth;

        public WorldCoordinates(int row, int col, int depth)
        {
            Row = row;
            Col = col;
            Depth = depth;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is WorldCoordinates other))
                return false;

            return Row == other.Row &&
                   Col == other.Col &&
                   Depth == other.Depth;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col, Depth);
        }
    }
}