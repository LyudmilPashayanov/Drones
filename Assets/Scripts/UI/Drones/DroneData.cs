using System;
using UnityEngine;

namespace UI.Drones
{
    public class DroneData
    {
        public string Name;
        public Color Color;
        private DroneState _state;
        public DroneState State
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnDroneDataUpdated?.Invoke(this);
                }
            }
        }

        private string _assignedJobId;
        public string AssignedJobId
        {
            get => _assignedJobId;
            set
            {
                if (_assignedJobId != value)
                {
                    _assignedJobId = value;
                    OnDroneDataUpdated?.Invoke(this);
                }
            }
        }

        public event Action<DroneData> OnDroneDataUpdated;
        
        public DroneData(string name, Color color, DroneState state, string  assignedJobId)
        {
            Name = name;
            Color = color;
            State = DroneState.Idle;
            AssignedJobId = assignedJobId;
        }
    }
}
