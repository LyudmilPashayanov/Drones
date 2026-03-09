using TMPro;
using UI.Drones;
using UnityEngine;
using UnityEngine.UI;

public class DroneListItemView : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text stateText;
    public TMP_Text assignedJobText;
    public Image colorImage;

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
