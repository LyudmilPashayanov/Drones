using System.Collections.Generic;
using World;

namespace Pathfinding
{
    public interface IPathfinder
    {
        List<WorldCoordinates> FindPath(WorldCoordinates start, WorldCoordinates goal);
    }
}