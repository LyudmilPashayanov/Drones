using System.Collections.Generic;
using UnityEngine;

public class WorldGrid
{
    private Dictionary<WorldCoordinates, WorldBlock> _blocks
        = new Dictionary<WorldCoordinates, WorldBlock>();

    public void Register(WorldBlock block)
    {
        _blocks[block.WorldPosition] = block;
    }

    public bool IsWalkable(WorldCoordinates coord)
    {
        if (!_blocks.TryGetValue(coord, out var block))
        {
            return false;
        }

        return !block.IsBlocked;
    }

    public WorldBlock Get(WorldCoordinates coord)
    {
        _blocks.TryGetValue(coord, out var block);
        return block;
    }
}
