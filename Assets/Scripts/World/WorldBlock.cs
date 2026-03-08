using UnityEngine;

public class WorldBlock : MonoBehaviour
{
   private WorldCoordinates _worldPosition;
   [SerializeField] private bool _isBlocked = false;
   [SerializeField] private GameObject _blocker;

   public bool IsBlocked => _isBlocked;
   public WorldCoordinates WorldPosition => _worldPosition;
   
   public void Initialize(WorldCoordinates coordinates, bool isBlocked)
   {
      _worldPosition = coordinates;
      
      if (isBlocked)
      {
         Block();
      }
   }

   private void Block()
   {
      _isBlocked = true;
      _blocker.SetActive(true);
   }
}
