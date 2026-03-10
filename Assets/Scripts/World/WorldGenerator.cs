using UnityEngine;
using VContainer;

namespace World
{
    public class WorldGenerator : MonoBehaviour
    {
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
            for (int row = -5; row < 5; row++)
            {
                for (int col = -5; col < 5; col++)
                {
                    for (int depth = -5; depth < 5; depth++)
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