using System.Collections.Generic;
using VContainer;

namespace Pathfinding
{
    public class StepCoordinator
    {
        private TrafficController _traffic;

        private List<Drone> _drones = new();

        private int _remainingMoves;

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

        public void StartSimulation()
        {
            ExecuteStep();
        }

        private void ExecuteStep()
        {
            _traffic.ClearReservations();

            _remainingMoves = 0;

            foreach (var drone in _drones)
            {
                if (drone.HasFinished)
                    continue;

                _remainingMoves++;

                drone.OnMoveCompleted += OnDroneFinished;
                drone.AttemptMove();
            }

            if (_remainingMoves == 0)
                UnityEngine.Debug.Log("All drones reached destinations.");
        }

        private void OnDroneFinished(Drone drone)
        {
            drone.OnMoveCompleted -= OnDroneFinished;

            _remainingMoves--;

            if (_remainingMoves <= 0)
            {
                ExecuteStep();
            }
        }
    }
}
