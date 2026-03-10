using System;
using System.Collections.Generic;
using Pathfinding;
using UI.Drones;
using World;

namespace Core
{
    public class Drone
    {
        private readonly IPathfinder _pathfinder;
        private readonly TrafficController _traffic;

        private readonly DroneData _droneData;
        public DroneData GetDroneData() => _droneData;

        private WorldCoordinates _position;
        private List<WorldCoordinates> _path;
        private int _pathIndex;
        private Job _currentJob;
        private DroneState _state = DroneState.Idle;

        public bool HasFinished
        {
            get
            {
                if (_currentJob != null) return false;
                if (_path == null) return true;
                return _pathIndex >= _path.Count;
            }
        }

        public event Action<WorldCoordinates> OnMoveRequested;
        public event Action<Drone> OnMoveCompleted;

        public Drone(IPathfinder pathfinder, TrafficController traffic, WorldCoordinates start, DroneData initialData)
        {
            _pathfinder = pathfinder;
            _traffic = traffic;
            _position = start;
            _droneData = initialData;
        }

        public void AssignJob(Job job)
        {
            if (_state != DroneState.Idle)
            {
                return;
            }

            ResetState();

            _currentJob = job;
            _currentJob.AssignedDrone = this;

            _droneData.AssignedJobId = _currentJob.Id;

            SetDroneState(DroneState.MovingToPickup);
            SetJobState(JobStatus.Assigned);


            SetDestination(job.Pickup);
        }

        private void SetDestination(WorldCoordinates target)
        {
            _path = _pathfinder.FindPath(_position, target);
            _pathIndex = 0;
        }

        // Single-step movement
        public void Step()
        {
            if (_path == null || _pathIndex >= _path.Count)
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

            _position = next;
            _pathIndex++;

            OnMoveRequested?.Invoke(next);
        }

        private void DestinationReached()
        {
            if (_currentJob == null)
            {
                SetDroneState(DroneState.Idle);
                return;
            }

            if (_state == DroneState.MovingToPickup)
            {
                SetDroneState(DroneState.MovingToDropoff);
                SetDestination(_currentJob.Dropoff);
            }
            else if (_state == DroneState.MovingToDropoff)
            {
                SetJobState(JobStatus.Completed);
                SetDroneState(DroneState.Idle);
                ResetState();
            }
        }

        private void ResetState()
        {
            _currentJob = null;
            _path = null;
            _pathIndex = 0;
        }

        public void StepCompleted()
        {
            OnMoveCompleted?.Invoke(this);
        }

        private void SetDroneState(DroneState state)
        {
            _state = state;
            _droneData.State = state;
        }

        private void SetJobState(JobStatus status)
        {
            _currentJob.Status = status;
        }
    }
}