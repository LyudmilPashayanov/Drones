using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JobsListItemView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text pickupText;
    [SerializeField] private TMP_Text dropoffText;
    [SerializeField] private TMP_Text stateText;

    [SerializeField] private Image selectedImage;
  
    private Job _job;
    private JobsViewModel _jobsVM;
    
    public void Initialize(Job job, JobsViewModel jobsVM)
    {
        _job = job;
        _jobsVM = jobsVM;
        SetData(new JobData(job));
        _jobsVM.JobSelected += OnJobSelected;
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
        _jobsVM.SelectedJob = _job;
    }
}
