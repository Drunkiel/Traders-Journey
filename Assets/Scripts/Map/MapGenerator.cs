using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance;
    public static bool isMapGenerated;
    public static Vector2Int mapSize = new Vector2Int(18, 10);

    public MapData _mapData;
    public Tilemap groundTilemap;
    public Tilemap waterTilemap;

    [SerializeField] private GroundGenerator _groundGenerator;
    [SerializeField] private LakeGenerator _lakeGenerator;
    [SerializeField] private EnvironmentGenerator _environmentGenerator;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _groundGenerator.GenerateGround();
        _lakeGenerator.GenerateLake();
        _environmentGenerator.GenerateEnvironment();
    }

    public int GetRandomTile(int numberOfTiles)
    {
        if (Random.Range(0, 4) == 0) return Random.Range(1, numberOfTiles);

        return 0;
    }
}
