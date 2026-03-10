using Core;
using UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using World;

namespace UI.ControlPanel
{
   public class ControlPanelView : MonoBehaviour
   {
      [SerializeField] private CoordinatesInputField pickUpInputField;
      [SerializeField] private CoordinatesInputField dropOffInputField;
      [SerializeField] private Button createJobButton;
      [SerializeField] private Button assignJobButton;

      private JobsViewModel _jobsVm;
      private DronesViewModel _dronesVm;
   
      private int _jobCounter = 0;

      [Inject]
      public void Construct(JobsViewModel jobsVm, DronesViewModel dronesVm)
      {
         _jobsVm = jobsVm;
         _dronesVm = dronesVm;
      }
   
      private void Start()
      {
         createJobButton.onClick.AddListener(CreateJob);
         assignJobButton.onClick.AddListener(AssignSelectedJobToSelectedDrone);
      }

      private void CreateJob()
      {
         WorldCoordinates pickUp = pickUpInputField.GetInputCoordinates();
         WorldCoordinates dropOff = dropOffInputField.GetInputCoordinates();
         _jobCounter++;
         string jobName = "job_" + _jobCounter; 
     
         Job job = new Job(
            jobName,
            pickUp,
            dropOff  
         );
      
         _jobsVm.AddJob(job);
      }
   
      private void AssignSelectedJobToSelectedDrone()
      {
         var drone = _dronesVm.SelectedDrone;
         var job = _jobsVm.SelectedJob;

         if (drone != null && job != null && drone.GetDroneData().State == DroneState.Idle && job.Status == JobStatus.Pending)
         {
            drone.AssignJob(job);
         }
      }
   
      private void OnDestroy()
      {
         createJobButton.onClick.RemoveListener(CreateJob);
         assignJobButton.onClick.RemoveListener(AssignSelectedJobToSelectedDrone);
      }
   }
}
