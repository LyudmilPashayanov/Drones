using System;
using World;

namespace Core
{
    public class Job
    {
        public string Id;
        public WorldCoordinates Pickup;
        public WorldCoordinates Dropoff;
        
        public event Action<Job> OnJobUpdated;

        private JobStatus _status;

        public JobStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnJobUpdated?.Invoke(this);
                }
            }
        }

        private Drone _assignedDrone;

        public Drone AssignedDrone
        {
            get => _assignedDrone;
            set
            {
                if (_assignedDrone != value)
                {
                    _assignedDrone = value;
                    OnJobUpdated?.Invoke(this);
                }
            }
        }
        
        public Job(string id, WorldCoordinates pick, WorldCoordinates drop)
        {
            Id = id;
            Pickup = pick;
            Dropoff = drop;
            Status = JobStatus.Pending;
        }
    }
}
