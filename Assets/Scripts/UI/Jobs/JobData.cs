using Core;

namespace UI.Jobs
{
    public struct JobData
    {
        public string Id;
        public string PickupText;
        public string DropoffText;
        public string Status;

        public JobData(Job job)
        {
            Id = job.Id;
            PickupText = $"({job.Pickup.Row}, {job.Pickup.Col}, {job.Pickup.Depth})";
            DropoffText = $"({job.Dropoff.Row}, {job.Dropoff.Col}, {job.Dropoff.Depth})";
            Status = job.Status.ToString();
        }
    }
}