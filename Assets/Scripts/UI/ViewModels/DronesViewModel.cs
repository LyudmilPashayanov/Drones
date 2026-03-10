using System;
using System.Collections.Generic;
using Core;
using UI.Drones;

namespace UI.ViewModels
{
    public class DronesViewModel
    {
        private readonly Dictionary<DroneData, Drone> _droneLookup = new();

        public event Action<DroneData> DroneAdded;
        public event Action<DroneData> DroneStateUpdated;
    
        public event Action<Drone> DroneSelected; 

        private Drone _selectedDrone;
        public Drone SelectedDrone
        {
            get => _selectedDrone;
            set
            {
                _selectedDrone = value;
                DroneSelected?.Invoke(_selectedDrone);
            }
        }
    
        public void AddDrone(DroneData droneData, Drone drone)
        {
            _droneLookup[droneData] = drone;
            droneData.OnDroneDataUpdated += UpdateDroneData;
            DroneAdded?.Invoke(droneData);
        }
    
        public Drone GetDroneFromData(DroneData data)
        {
            return _droneLookup.TryGetValue(data, out var drone) ? drone : null;
        }
    
        private void UpdateDroneData(DroneData droneDataToUpdate)
        {
            DroneStateUpdated?.Invoke(droneDataToUpdate);
        }

        public void SelectDrone(DroneData droneData)
        {
            SelectedDrone = _droneLookup.TryGetValue(droneData, out var drone) ? drone : null;
        }
    }
}
