using TMPro;
using UI.Drones;
using UnityEngine;
using UnityEngine.UI;

public class DroneListItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text stateText;
    [SerializeField] private TMP_Text assignedJobText;
    [SerializeField] private Image colorImage;

    public void SetData(DroneData info)
    {
        nameText.text = info.Name;
        colorImage.color = info.Color;
        stateText.text = info.State.ToString();
    }

    public void UpdateState(string state)
    {
        stateText.text = state;
    }
}
