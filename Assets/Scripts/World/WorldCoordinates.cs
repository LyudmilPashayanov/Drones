using System;

public struct WorldCoordinates
{
    public int row, col, depth;

    public WorldCoordinates(int row, int col, int depth)
    {
        this.row = row;
        this.col = col;
        this.depth = depth;
    }
    
    public override bool Equals(object obj)
    {
        if (!(obj is WorldCoordinates other))
            return false;

        return row == other.row &&
               col == other.col &&
               depth == other.depth;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(row, col, depth);
    }
}
