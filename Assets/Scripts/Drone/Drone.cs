using System;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UI.Drones;
using UnityEngine;

public class Drone : MonoBehaviour // Make this pure C# and move the unity logic to a diff class.
{
    //TODO: Split the class in several subclasses- drone engine (moving) & drone job doer (jobs)
    
    [SerializeField] private float _moveDuration = 0.35f;
    [SerializeField] private MeshRenderer _droneRenderer;
    private IPathfinder _pathfinder;
    private TrafficController _traffic;
    private DronesViewModel _droneVM;
    
    private Job _currentJob;
    private bool _headingToPickup;
    
    private DroneData _droneData;
    
    private List<WorldCoordinates> _path;
    private int _pathIndex;

    public bool HasFinished => _path == null || _pathIndex >= _path.Count;
    public event Action<Drone> OnMoveCompleted;

    public void Initialize(IPathfinder pathfinder, TrafficController traffic, DronesViewModel droneVM, DroneData droneData)
    {
        _pathfinder = pathfinder;
        _traffic = traffic;
        _droneVM = droneVM;
        ConstructDrone(droneData); // Move this to a separate class only the visual representation of the drone?
    }
    
    public void AssignJob(Job assignedJob)
    {
        if (_droneData.State != DroneState.Idle || _currentJob != null)
            return;

        _currentJob = assignedJob;
        _droneData.State = DroneState.ExecutingJob;
        _headingToPickup = true;
        
        assignedJob.Status = JobStatus.Assigned;
        assignedJob.AssignedDrone = this;
        
        SetDestination(_currentJob.Pickup);
    }
    
    private void ConstructDrone(DroneData droneData)
    {
        _droneData =  droneData;
        
        gameObject.name = droneData.Name;
        _droneRenderer.material.color = droneData.Color;
    }
    
    private void SetDestination(WorldCoordinates target)
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
            DestinationReached();
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

    private void DestinationReached()
    {
        if (_currentJob == null)
        {
            return;
        }
        
        switch (_droneData.State)
        {
            case DroneState.MovingToPickup:
            {
                _droneData.State = DroneState.MovingToDropoff;
                SetDestination(_currentJob.Dropoff);

                break;
            }
            case DroneState.MovingToDropoff:
            {
                _currentJob.Status = JobStatus.Completed;
                _droneData.State = DroneState.Idle;
                _currentJob = null;

                break;
            }
        }
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
