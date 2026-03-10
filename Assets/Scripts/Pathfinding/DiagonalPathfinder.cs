using System.Collections.Generic;
using UnityEngine;
using World;

namespace Pathfinding
{
    public class DiagonalPathfinder : IPathfinder
    {
        private readonly WorldGrid _grid;

        public DiagonalPathfinder(WorldGrid grid)
        {
            _grid = grid;
        }

        public List<WorldCoordinates> FindPath(WorldCoordinates start, WorldCoordinates goal)
        {
            if (!_grid.IsWalkable(goal))
            {
                return null;
            }

            var openSet = new PriorityQueue<WorldCoordinates>();
            var closedSet = new HashSet<WorldCoordinates>();

            var cameFrom = new Dictionary<WorldCoordinates, WorldCoordinates>();

            var gScore = new Dictionary<WorldCoordinates, float>();
            gScore[start] = 0;

            openSet.Enqueue(start, Heuristic(start, goal));

            while (openSet.Count > 0)
            {
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

                    float tentativeG = gScore[current] + 1;

                    if (!gScore.TryGetValue(neighbor, out float existing) || tentativeG < existing)
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
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        if (dx == 0 && dy == 0 && dz == 0)
                            continue;

                        yield return new WorldCoordinates
                        {
                            Row = c.Row + dx,
                            Col = c.Col + dy,
                            Depth = c.Depth + dz
                        };
                    }
                }
            }
        }

        private float Heuristic(WorldCoordinates a, WorldCoordinates b)
        {
            float dx = a.Row - b.Row;
            float dy = a.Col - b.Col;
            float dz = a.Depth - b.Depth;
            return Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        private List<WorldCoordinates> ReconstructPath(
            Dictionary<WorldCoordinates, WorldCoordinates> cameFrom,
            WorldCoordinates current)
        {
            List<WorldCoordinates> path = new();

            path.Add(current);

            while (cameFrom.TryGetValue(current, out var prev))
            {
                current = prev;
                path.Add(current);
            }

            path.Reverse();
            return path;
        }
    }
}