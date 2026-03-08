using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class StraightPathfinder : IPathfinder
    {
        private readonly WorldGrid _grid;

        public StraightPathfinder(WorldGrid grid)
        {
            _grid = grid;
        }

        public List<WorldCoordinates> FindPath(WorldCoordinates start, WorldCoordinates goal)
        {
            if (!_grid.IsWalkable(start))
                return null;

            if (!_grid.IsWalkable(goal))
                return null;

            var openSet = new PriorityQueue<WorldCoordinates>();
            var closedSet = new HashSet<WorldCoordinates>();

            var cameFrom = new Dictionary<WorldCoordinates, WorldCoordinates>();
            var gScore = new Dictionary<WorldCoordinates, float>();

            gScore[start] = 0f;
            openSet.Enqueue(start, Heuristic(start, goal));

            int safetyCounter = 0;

            while (openSet.Count > 0)
            {
                safetyCounter++;
                if (safetyCounter > 100000)
                {
                    Debug.LogError("StraightPathfinder: Too many iterations (possible loop)");
                    return null;
                }

                WorldCoordinates current = openSet.Dequeue();

                if (current.Equals(goal))
                    return ReconstructPath(cameFrom, current);

                closedSet.Add(current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    if (!_grid.IsWalkable(neighbor))
                        continue;

                    float tentativeG = gScore[current] + 1f; // straight movement cost

                    if (!gScore.TryGetValue(neighbor, out float existingCost) ||
                        tentativeG < existingCost)
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeG;

                        float fScore = tentativeG + Heuristic(neighbor, goal);
                        openSet.Enqueue(neighbor, fScore);
                    }
                }
            }

            return null;
        }

        private IEnumerable<WorldCoordinates> GetNeighbors(WorldCoordinates c)
        {
            yield return new WorldCoordinates { row = c.row + 1, col = c.col, depth = c.depth };
            yield return new WorldCoordinates { row = c.row - 1, col = c.col, depth = c.depth };

            yield return new WorldCoordinates { row = c.row, col = c.col + 1, depth = c.depth };
            yield return new WorldCoordinates { row = c.row, col = c.col - 1, depth = c.depth };

            yield return new WorldCoordinates { row = c.row, col = c.col, depth = c.depth + 1 };
            yield return new WorldCoordinates { row = c.row, col = c.col, depth = c.depth - 1 };
        }
    
        private float Heuristic(WorldCoordinates a, WorldCoordinates b)
        {
            return Mathf.Abs(a.row - b.row) +
                   Mathf.Abs(a.col - b.col) +
                   Mathf.Abs(a.depth - b.depth);
        }

        private List<WorldCoordinates> ReconstructPath(
            Dictionary<WorldCoordinates, WorldCoordinates> cameFrom,
            WorldCoordinates current)
        {
            var path = new List<WorldCoordinates>();
            path.Add(current);

            while (cameFrom.TryGetValue(current, out var previous))
            {
                current = previous;
                path.Add(current);
            }

            path.Reverse();
            return path;
        }
    }
}
