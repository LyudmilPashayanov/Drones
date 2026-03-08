using System.Collections.Generic;

namespace Pathfinding
{
    public interface IPathfinder
    {
        List<WorldCoordinates> FindPath(WorldCoordinates start, WorldCoordinates goal);
    }
}