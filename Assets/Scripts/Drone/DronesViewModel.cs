using System;
using System.Collections.Generic;
using UI.Drones;

public class DronesViewModel
{
    private readonly List<DroneData> _drones = new();

    public IReadOnlyList<DroneData> Drones => _drones;

    public event Action<DroneData> DroneAdded;
    public event Action<DroneData> DroneStateUpdated;

    public void AddDrone(DroneData drone)
    {
        _drones.Add(drone);
        DroneAdded?.Invoke(drone);
    }

    public void UpdateDroneData(DroneData droneDataToUpdate)
    {
        DroneStateUpdated?.Invoke(droneDataToUpdate);
    }
}
