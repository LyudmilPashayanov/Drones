using System;
using System.Collections.Generic;

public class JobsViewModel
{
    private readonly List<Job> _jobs = new();
    public IReadOnlyList<Job> Jobs => _jobs;

    public event Action<Job> JobAdded;
    public event Action<Job> JobUpdated;

    public void AddJob(Job job)
    {
        _jobs.Add(job);
        JobAdded?.Invoke(job);
    }

    public void UpdateJob(Job job)
    {
        JobUpdated?.Invoke(job);
    }
}
