using UnityEngine;
using VContainer;

namespace World
{
    public class WorldGenerator : MonoBehaviour
    {
        private const int WORLD_BOUNDS = 5;
        
        private WorldGrid _grid;

        [SerializeField] private Transform spawnLocation;
        [SerializeField] private WorldBlock blockPrefab;

        [Inject]
        public void Construct(WorldGrid grid)
        {
            _grid = grid;
        }

        private void Awake()
        {
            GenerateWorld();
        }

        private void GenerateWorld()
        {
            for (int row = -WORLD_BOUNDS; row <= WORLD_BOUNDS; row++)
            {
                for (int col = -WORLD_BOUNDS; col <= WORLD_BOUNDS; col++)
                {
                    for (int depth = -WORLD_BOUNDS; depth <= WORLD_BOUNDS; depth++)
                    {
                        bool blocked = false;
                        string blockName = row + "_" + col + "_" + depth;
                        WorldCoordinates coordinates = new WorldCoordinates() { Col = col, Row = row, Depth = depth };

                        WorldBlock newBlock = Instantiate(blockPrefab, new Vector3(row, col, depth),
                            Quaternion.identity, spawnLocation);
                        newBlock.gameObject.name = blockName;
                        if (row == 0)
                        {
                            blocked = true;
                            if (col == 0)
                            {
                                blocked = false;
                            }
                        }

                        newBlock.Initialize(coordinates, blocked);
                        _grid.Register(newBlock);
                    }
                }
            }
        }
    }
}