using Pathfinding;
using UI.Drones;
using UI.ViewModels;
using UnityEngine;
using VContainer;
using World;

namespace Core
{
    public class DroneFactory : MonoBehaviour
    {
        [SerializeField] private DroneAgent dronePrefab;
        [SerializeField] private int droneSpawnsCount = 10;

        private IPathfinder _pathfinder;
        private TrafficController _traffic;
        private StepCoordinator _coordinator;
        private DronesViewModel _dronesVm;
        
        private int _droneCounter;

        [Inject]
        public void Construct(
            IPathfinder pathfinder,
            TrafficController traffic,
            StepCoordinator coordinator,
            DronesViewModel dronesVm)
        {
            _pathfinder = pathfinder;
            _traffic = traffic;
            _coordinator = coordinator;
            _dronesVm = dronesVm;
        }

        void Start()
        {
            for (int i = 0; i < droneSpawnsCount; i++)
            {
                CreateDrone(new Vector3Int(-4 + i, 0, 0));
            }
        }

        private Drone CreateDrone(Vector3Int position)
        {
            var startCoord = new WorldCoordinates
            {
                Row = position.x,
                Col = position.y,
                Depth = position.z
            };

            DroneData newDroneData = GenerateNextDroneData();
            Drone drone = new Drone(_pathfinder, _traffic, startCoord, newDroneData);
            DroneAgent agent = Instantiate(dronePrefab, position, Quaternion.identity);

            agent.Initialize(drone, newDroneData);


            _dronesVm.AddDrone(newDroneData, drone);

            _coordinator.RegisterDrone(drone);
            return drone;
        }

        private DroneData GenerateNextDroneData()
        {
            Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            DroneData droneData = new DroneData(
                $"Drone {_droneCounter++}",
                randomColor,
                DroneState.Idle,
                string.Empty
            );
            return droneData;
        }
    }
}