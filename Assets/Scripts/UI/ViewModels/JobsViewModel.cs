using System;
using System.Collections.Generic;
using Core;

namespace UI.ViewModels
{
    public class JobsViewModel
    {
        private readonly List<Job> _jobs = new();
        public IReadOnlyList<Job> Jobs => _jobs;

        public event Action<Job> JobAdded;
        public event Action<Job> JobUpdated;
        public event Action<Job> JobSelected; // UI selection

        private Job _selectedJob;
        public Job SelectedJob
        {
            get => _selectedJob;
            set
            {
                _selectedJob = value;
                JobSelected?.Invoke(_selectedJob);
            }
        }
    
        public void AddJob(Job job)
        {
            _jobs.Add(job);
            job.OnJobUpdated += UpdateJob;
            JobAdded?.Invoke(job);
        }

        private void UpdateJob(Job job)
        {
            JobUpdated?.Invoke(job);
        }
    }
}
