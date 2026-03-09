using TMPro;
using UnityEngine;

public class CoordinatesInputField : MonoBehaviour
{
    [SerializeField] TMP_InputField _rowInputField;
    [SerializeField] TMP_InputField _colInputField;
    [SerializeField] TMP_InputField _depthInputField;

    private void Start()
    {
        _rowInputField.text = "0";
        _colInputField.text = "0";
        _depthInputField.text = "0";
    }

    public WorldCoordinates GetInputCoordinates()
    {
        int row = int.Parse(_rowInputField.text);
        int col = int.Parse(_colInputField.text);
        int depth = int.Parse(_depthInputField.text);
        return new WorldCoordinates(row, col, depth);
    }
}
