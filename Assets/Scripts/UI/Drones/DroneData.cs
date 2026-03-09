using UnityEngine;

namespace UI.Drones
{
    public struct DroneData
    {
        public string Name;
        public Color Color;
        public DroneState State;

        public DroneData(string name, Color color, DroneState state)
        {
            Name = name;
            Color = color;
            State = DroneState.Idle;
        }
    }
}
