using Core;
using TMPro;
using UI.ViewModels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Jobs
{
    public class JobsListItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text pickupText;
        [SerializeField] private TMP_Text dropoffText;
        [SerializeField] private TMP_Text stateText;

        [SerializeField] private Image selectedImage;
  
        private Job _job;
        private JobsViewModel _jobsVm;
    
        public void Initialize(Job job, JobsViewModel jobsVm)
        {
            _job = job;
            _jobsVm = jobsVm;
            SetData(new JobData(job));
            _jobsVm.JobSelected += OnJobSelected;
        }
    
        private void SetData(JobData data)
        {
            nameText.text = $"{data.Id}";
            pickupText.text = $"{data.PickupText}";
            dropoffText.text = $"{data.DropoffText}";
            stateText.text = data.Status;
        }

        public void UpdateState(string status)
        {
            stateText.text = status;
        }

        private void OnJobSelected(Job selected)
        {
            selectedImage.gameObject.SetActive(_job == selected);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _jobsVm.SelectedJob = _job;
        }
    }
}
