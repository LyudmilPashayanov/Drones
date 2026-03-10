using System.Collections.Generic;
using UI.ViewModels;
using UnityEngine;
using VContainer;

namespace UI.Drones
{
    public class DroneListView : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private DroneListItemView itemPrefab;

        private Dictionary<DroneData, DroneListItemView> _droneItems = new();
        private DronesViewModel _vm;

        [Inject]
        public void Construct(DronesViewModel vm)
        {
            _vm = vm;
        }

        void Start()
        {
            _vm.DroneAdded += AddItem;
            _vm.DroneStateUpdated += UpdateDroneState;
        }

        private void UpdateDroneState(DroneData obj)
        {
            _droneItems[obj].UpdateState(obj);
        }

        void AddItem(DroneData info)
        {
            DroneListItemView droneItem = Instantiate(itemPrefab, content);
            droneItem.Initialize(info, _vm);
            _droneItems.Add(info, droneItem);
            ResizeScrollView(droneItem.GetComponent<RectTransform>());
        }

        private void ResizeScrollView(RectTransform droneRect)
        {
            Vector2 currentSize = content.sizeDelta;
            currentSize.y += droneRect.rect.height + 20;
            content.sizeDelta = currentSize;
            
            droneRect.anchoredPosition = new Vector2(droneRect.anchoredPosition.x, -(content.sizeDelta.y - droneRect.rect.height - 20));
        }
    }
}
