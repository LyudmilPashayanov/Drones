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
            
           
            //_coordinator.StartSimulation();
        }
    }
}
