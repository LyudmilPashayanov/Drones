using System.Collections.Generic;

namespace Pathfinding
{
    public class TrafficController
    {
        private HashSet<WorldCoordinates> _reservedCells = new();
        private HashSet<(WorldCoordinates from, WorldCoordinates to)> _reservedEdges = new();

        public bool TryReserve(WorldCoordinates from, WorldCoordinates to)
        {
            if (_reservedCells.Contains(to))
                return false;

            if (_reservedEdges.Contains((to, from)))
                return false;

            _reservedCells.Add(to);
            _reservedEdges.Add((from, to));

            return true;
        }

        public void ClearReservations()
        {
            _reservedCells.Clear();
            _reservedEdges.Clear();
        }
    }
}