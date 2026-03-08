using System;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private float _moveDuration = 0.35f;
    private IPathfinder _pathfinder;
    private TrafficController _traffic;
    
    private List<WorldCoordinates> _path;
    private int _pathIndex;

    public bool HasFinished => _path == null || _pathIndex >= _path.Count;
    public event Action<Drone> OnMoveCompleted;

    public void Initialize(IPathfinder pathfinder, TrafficController traffic)
    {
        _pathfinder = pathfinder;
        _traffic = traffic;
    }

    public void SetDestination(WorldCoordinates target)
    {
        WorldCoordinates start = GetCurrentCoordinate();

        _path = _pathfinder.FindPath(start, target);
        _pathIndex = 0;
    }

    public void AttemptMove()
    {
        if (HasFinished)
        {
            OnMoveCompleted?.Invoke(this);
            return;
        }

        WorldCoordinates from = GetCurrentCoordinate();
        WorldCoordinates to = _path[_pathIndex];

        if (!_traffic.TryReserve(from, to))
        {
            OnMoveCompleted?.Invoke(this);
            return;
        }

        MoveAnimated(to);
    }

    private void MoveAnimated(WorldCoordinates coord)
    {
        Vector3 target = CoordinateToWorld(coord);

        transform.DOMove(target, _moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _pathIndex++;
                OnMoveCompleted?.Invoke(this);
            });
    }

    private WorldCoordinates GetCurrentCoordinate()
    {
        Vector3 pos = transform.position;

        return new WorldCoordinates
        {
            row = Mathf.RoundToInt(pos.x),
            col = Mathf.RoundToInt(pos.y),
            depth = Mathf.RoundToInt(pos.z)
        };
    }

    private Vector3 CoordinateToWorld(WorldCoordinates coord)
    {
        return new Vector3(coord.row, coord.col, coord.depth);
    }
}
