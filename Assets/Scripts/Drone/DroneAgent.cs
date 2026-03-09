using DG.Tweening;
using UI.Drones;
using UnityEngine;

public class DroneAgent : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private float _moveDuration = 0.35f;

    private Drone _drone;

    public void Initialize(Drone drone, DroneData data)
    {
        _drone = drone;

        gameObject.name = data.Name;
        _renderer.material.color = data.Color;

        _drone.OnMoveRequested += MoveTo;
    }

    private void MoveTo(WorldCoordinates coord)
    {
        Vector3 target = new Vector3(coord.row, coord.col, coord.depth);

        transform.DOMove(target, _moveDuration)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                // Notify the drone that its step is completed
                _drone.StepCompleted();
            });;
    }
}