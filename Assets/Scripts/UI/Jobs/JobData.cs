using UnityEngine;

public struct JobData
{
    public string Id;
    public string PickupText;
    public string DropoffText;
    public string Status;

    public JobData(Job job)
    {
        Id = job.Id;
        PickupText = $"({job.Pickup.row}, {job.Pickup.col}, {job.Pickup.depth})";
        DropoffText = $"({job.Dropoff.row}, {job.Dropoff.col}, {job.Dropoff.depth})";
        Status = job.Status.ToString();
    }
}