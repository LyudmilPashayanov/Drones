public class Job
{
    public string Id;
    public WorldCoordinates Pickup;
    public WorldCoordinates Dropoff;
    public JobStatus Status;

    public Drone AssignedDrone;
    
    public Job(string id, WorldCoordinates pick, WorldCoordinates drop)
    {
        Id = id;
        Pickup = pick;
        Dropoff = drop;
        Status = JobStatus.Pending;
    }
}
