using TMPro;
using UnityEngine;
using World;

namespace UI.ControlPanel
{
    public class CoordinatesInputField : MonoBehaviour
    {
        [SerializeField] private TMP_InputField rowInputField;
        [SerializeField] private TMP_InputField colInputField;
        [SerializeField] private TMP_InputField depthInputField;

        private void Start()
        {
            rowInputField.text = "0";
            colInputField.text = "0";
            depthInputField.text = "0";
        }

        public WorldCoordinates GetInputCoordinates()
        {
            int row = int.Parse(rowInputField.text);
            int col = int.Parse(colInputField.text);
            int depth = int.Parse(depthInputField.text);
            return new WorldCoordinates(row, col, depth);
        }
    }
}
