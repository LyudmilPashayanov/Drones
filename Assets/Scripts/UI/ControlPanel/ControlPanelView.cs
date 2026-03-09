using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ControlPanelView : MonoBehaviour
{
   [SerializeField] private CoordinatesInputField PickUpInputField;
   [SerializeField] private CoordinatesInputField DropOffInputField;
   [SerializeField] private Button CreateJobButton;
   [SerializeField] private Button AssignJobButton;

   private JobsViewModel _jobsVM;
   private DronesViewModel _dronesVM;
   
   private int jobCounter = 0;

   [Inject]
   public void Construct(JobsViewModel jobsVM, DronesViewModel dronesVM)
   {
      _jobsVM = jobsVM;
      _dronesVM = dronesVM;
   }
   
   private void Start()
   {
      CreateJobButton.onClick.AddListener(CreateJob);
      AssignJobButton.onClick.AddListener(AssignSelectedJobToSelectedDrone);
   }

   private void CreateJob()
   {
      WorldCoordinates pickUp = PickUpInputField.GetInputCoordinates();
      WorldCoordinates dropOff = DropOffInputField.GetInputCoordinates();
      jobCounter++;
      string jobName = "job_" + jobCounter; 
     
      Job job = new Job(
         jobName,
         pickUp,
         dropOff  
      );
      
      _jobsVM.AddJob(job);
   }
   
   private void AssignSelectedJobToSelectedDrone()
   {
      var drone = _dronesVM.SelectedDrone;
      var job = _jobsVM.SelectedJob;

      if (drone != null && job != null && drone.GetDroneData().State == DroneState.Idle && job.Status == JobStatus.Pending)
      {
         drone.AssignJob(job);
      }
   }
   
   private void OnDestroy()
   {
      CreateJobButton.onClick.RemoveListener(CreateJob);
      AssignJobButton.onClick.RemoveListener(AssignSelectedJobToSelectedDrone);
   }
}
