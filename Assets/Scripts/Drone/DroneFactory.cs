using Pathfinding;
using UnityEngine;
using VContainer;

public class DroneFactory : MonoBehaviour
{
    public Drone dronePrefab;
    public int count = 10;

    private IPathfinder _pathfinder;
    private TrafficController _traffic;
    private StepCoordinator _coordinator;

    [Inject]
    public void Construct(
        IPathfinder pathfinder,
        TrafficController traffic,
        StepCoordinator coordinator)
    {
        _pathfinder = pathfinder;
        _traffic = traffic;
        _coordinator = coordinator;
    }

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            CreateDrone(new Vector3(-4 + i, 0, 0));
        }

    }

    private Drone CreateDrone(Vector3 position)
    {
        Drone drone = Instantiate(dronePrefab, position, Quaternion.identity);

        drone.Initialize(_pathfinder, _traffic);

        _coordinator.RegisterDrone(drone);

        return drone;
    }
}
