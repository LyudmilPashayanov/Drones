using System.Collections.Generic;
using VContainer;

namespace Pathfinding
{
    public class StepCoordinator
    {
        private TrafficController _traffic;
        private readonly List<Drone> _drones = new();
        private int _remainingMoves;
        private bool _isRunning = false;

        [Inject]
        public void Construct(TrafficController traffic)
        {
            _traffic = traffic;
        }

        public void RegisterDrone(Drone drone)
        {
            if (!_drones.Contains(drone))
                _drones.Add(drone);
        }

        // Start simulation if not already running
        public void StartSimulation()
        {
            if (_isRunning) return;
            _isRunning = true;
            ExecuteStep();
        }

        // Single step for all drones
        private void ExecuteStep()
        {
            _traffic.ClearReservations();

            _remainingMoves = 0;

            var activeDrones = new List<Drone>();

            foreach (var drone in _drones)
            {
                if (drone.HasFinished)
                    continue;

                activeDrones.Add(drone);
            }

            _remainingMoves = activeDrones.Count;

            if (_remainingMoves == 0)
            {
                _isRunning = false;
                return;
            }

            // Subscribe first
            foreach (var drone in activeDrones)
            {
                drone.OnMoveCompleted += OnDroneFinished;
            }

            // Then execute moves
            foreach (var drone in activeDrones)
            {
                drone.Step();
            }
        }

        private void OnDroneFinished(Drone drone)
        {
            drone.OnMoveCompleted -= OnDroneFinished;
            _remainingMoves--;

            if (_remainingMoves <= 0)
            {
                // Only execute next step if there are still active drones
                bool anyMoving = _drones.Exists(d => !d.HasFinished);
                if (anyMoving)
                    ExecuteStep();
                else
                    _isRunning = false;
            }
        }
    }
}
