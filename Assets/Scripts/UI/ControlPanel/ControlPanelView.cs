using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ControlPanelView : MonoBehaviour
{
   [SerializeField] private CoordinatesInputField PickUpInputField;
   [SerializeField] private CoordinatesInputField DropOffInputField;
   [SerializeField] private Button CreateJobButton;

   [Inject] private JobsViewModel _jobsVM;
   private int jobCounter = 0;
   
   
   private void Start()
   {
      CreateJobButton.onClick.AddListener(CreateJob);
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
}
