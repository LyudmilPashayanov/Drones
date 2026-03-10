using UnityEngine;

namespace World
{
   public class WorldBlock : MonoBehaviour
   {
      private WorldCoordinates _worldPosition;
      [SerializeField] private bool isBlocked = false;
      [SerializeField] private GameObject blocker;

      public bool IsBlocked => isBlocked;
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
         isBlocked = true;
         blocker.SetActive(true);
      }
   }
}
