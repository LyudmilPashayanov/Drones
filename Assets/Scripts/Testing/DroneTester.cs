using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class DroneTester : MonoBehaviour
{
    private Drone[] _drones;
    private StepCoordinator _coordinator;
   
    [Inject]
    public void Construct(StepCoordinator coordinator)
    {
        _coordinator = coordinator;
    }
    
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _drones = FindObjectsOfType<Drone>();

            foreach (var drone in _drones)
            {
                WorldCoordinates target = new WorldCoordinates
                {
                    row = Random.Range(-4, 4),
                    col = Random.Range(-4, 4),
                    depth = Random.Range(-4, 4)
                };

                Debug.Log($"Moving drone to {target.row},{target.col},{target.depth}");

                //drone.SetDestination(target);
                
            }
           
            _coordinator.StartSimulation();
        }
    }
}
