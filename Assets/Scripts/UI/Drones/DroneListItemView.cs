using Core;
using TMPro;
using UI.ViewModels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Drones
{
    public class DroneListItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text stateText;
        [SerializeField] private TMP_Text assignedJobText;
        [SerializeField] private Image colorImage;
        [SerializeField] private Image selectedImage;

        private DroneData _droneData;
        private DronesViewModel _dronesVM;

        public void Initialize(DroneData droneData, DronesViewModel dronesVM)
        {
            _droneData = droneData;
            _dronesVM = dronesVM;

            UpdateState(droneData);
            _dronesVM.DroneSelected += OnDroneSelected;
        }

        public void UpdateState(DroneData info)
        {
            nameText.text = info.Name;
            colorImage.color = info.Color;
            stateText.text = info.State.ToString();
            assignedJobText.text = info.AssignedJobId;
        }

        private void OnDroneSelected(Drone selected)
        {
            selectedImage.gameObject.SetActive(_dronesVM.GetDroneFromData(_droneData) == selected);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _dronesVM.SelectDrone(_droneData);
        }
    }
}