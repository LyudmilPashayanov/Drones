using TMPro;
using UnityEngine;

public class JobsListItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text pickupText;
    [SerializeField] private TMP_Text dropoffText;
    [SerializeField] private TMP_Text stateText;

    public void SetData(JobData data)
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
}
