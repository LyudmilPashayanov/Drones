using System;
using System.Collections.Generic;
using Pathfinding;

public class Drone
{
    private readonly IPathfinder _pathfinder;
    private readonly TrafficController _traffic;

    private WorldCoordinates _position;

    private List<WorldCoordinates> _path;
    private int _pathIndex;

    private Job _currentJob;
    private DroneState _state = DroneState.Idle;

    public WorldCoordinates Position => _position;

    public bool HasFinished => _path == null || _pathIndex >= _path.Count;

    public event Action<WorldCoordinates> OnMoveRequested;
    public event Action<Drone> OnMoveCompleted;

    public Drone(IPathfinder pathfinder, TrafficController traffic, WorldCoordinates start)
    {
        _pathfinder = pathfinder;
        _traffic = traffic;
        _position = start;
    }

    public void AssignJob(Job job)
    {
        if (_state != DroneState.Idle)
            return;

        _currentJob = job;
        _state = DroneState.MovingToPickup;

        job.Status = JobStatus.Assigned;
        job.AssignedDrone = this;

        SetDestination(job.Pickup);
    }

    public void AttemptMove()
    {
        if (HasFinished)
        {
            DestinationReached();
            OnMoveCompleted?.Invoke(this);
            return;
        }

        var next = _path[_pathIndex];

        if (!_traffic.TryReserve(_position, next))
        {
            OnMoveCompleted?.Invoke(this);
            return;
        }

        _pathIndex++;
        _position = next;

        OnMoveRequested?.Invoke(next);
    }

    private void DestinationReached()
    {
        if (_currentJob == null)
            return;

        if (_state == DroneState.MovingToPickup)
        {
            _state = DroneState.MovingToDropoff;
            SetDestination(_currentJob.Dropoff);
        }
        else
        {
            _currentJob.Status = JobStatus.Completed;
            _currentJob = null;
            _state = DroneState.Idle;
        }
    }

    private void SetDestination(WorldCoordinates target)
    {
        _path = _pathfinder.FindPath(_position, target);
        _pathIndex = 0;
    }
}