using Core;
using Pathfinding;
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
      [SerializeField] private Button startSimulationButton;

      private JobsViewModel _jobsVm;
      private DronesViewModel _dronesVm;
      private StepCoordinator _coordinator;
      
      private int _jobCounter = 0;

      [Inject]
      public void Construct(JobsViewModel jobsVm, DronesViewModel dronesVm, StepCoordinator coordinator)
      {
         _jobsVm = jobsVm;
         _dronesVm = dronesVm;
         _coordinator = coordinator;
      }
   
      private void Start()
      {
         createJobButton.onClick.AddListener(CreateJob);
         assignJobButton.onClick.AddListener(AssignSelectedJobToSelectedDrone);
         startSimulationButton.onClick.AddListener(StartSimulation);

         CreatePremadeJobs();
      }

      private void StartSimulation()
      {
         _coordinator.StartSimulation();
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

      private void CreatePremadeJobs()
      {
         WorldCoordinates pickUp = new WorldCoordinates(-5,-5,-5);
         WorldCoordinates dropOff = new WorldCoordinates(5,5,5);
         string jobName = "job_corners"; 
     
         Job job = new Job(
            jobName,
            pickUp,
            dropOff  
         );
      
         _jobsVm.AddJob(job);
         
         WorldCoordinates pickUp2 = new WorldCoordinates(0,0,0);
         WorldCoordinates dropOff2 = new WorldCoordinates(2,-5,4);
         string jobName2 = "job_center_out"; 
     
         Job job2 = new Job(
            jobName2,
            pickUp2,
            dropOff2
         );
      
         _jobsVm.AddJob(job2);
      }
   }
}
