using UnityEngine;

public class LakeGenerator : MonoBehaviour
{
    public static bool isLakeGenerated;
    public GameObject walkerPrefab;
    private WalkerController _walkerController;

    public void GenerateLake()
    {
        for (int i = 0; i < MapGenerator.mapSize.x + MapGenerator.mapSize.y; i++)
        {
            SpawnWalker();
        }
    }

    private void SpawnWalker()
    {
        GameObject newWalker = Instantiate(walkerPrefab);
        _walkerController = newWalker.GetComponent<WalkerController>();
        _walkerController.tilemap = MapGenerator.instance.waterTilemap;
        _walkerController.GenerateMap(new Vector2Int(MapGenerator.mapSize.x * 20, MapGenerator.mapSize.y * 20), 1000);
    }
}
