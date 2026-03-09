using UnityEngine;
using VContainer;

public class WorldGenerator : MonoBehaviour
{
    private WorldGrid _grid;
    
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private WorldBlock _blockPrefab;

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
                    string Name = row + "_" + col + "_" + depth;
                    WorldCoordinates coordinates = new WorldCoordinates(){col = col, row = row, depth = depth};
                    
                    WorldBlock newBlock = Instantiate(_blockPrefab, new Vector3(row, col, depth), Quaternion.identity, _spawnLocation);
                    newBlock.gameObject.name = Name;
                    if (row == 0)
                    {
                        blocked = true;
                        if (col == 0 )
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
