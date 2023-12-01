using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentGenerator : MonoBehaviour
{
    public static bool isEnviromentGenerated;
    private Tilemap waterTilemap;
    [SerializeField] private int treesGenerated;
    [SerializeField] private int maxTrees;
    [SerializeField] private Transform parent;

    private void Start()
    {
        waterTilemap = MapGenerator.instance.waterTilemap;
        maxTrees = 20 * (ChunkController.singleChunkSize.x + ChunkController.singleChunkSize.y + MapGenerator.mapSize.x + MapGenerator.mapSize.y);
    }

    public void GenerateEnvironment()
    {
        waterTilemap = MapGenerator.instance.waterTilemap;
        StartCoroutine(GenerateEnvironment(0));
    }

    private IEnumerator GenerateEnvironment(float interval)
    {
        MapGenerator _mapGenerator = MapGenerator.instance;
        EnvironmentData _environmentData = _mapGenerator._mapData._environmentData;

        GameObject[] prefabs = _environmentData.treePrefabs;

        while (treesGenerated <= maxTrees)
        {
            if (LakeGenerator.isLakeGenerated)
            {
                Vector3 randomPosition = GetPosition();
                Instantiate(prefabs[_mapGenerator.GetRandomTile(prefabs.Length)], randomPosition, Quaternion.identity, parent);
                treesGenerated++;
            }
            yield return new WaitForSeconds(interval);
        }
        isEnviromentGenerated = true;
    }

    private Vector3 GetPosition()
    {
        Vector2Int mapSize = new Vector2Int(MapGenerator.mapSize.x * 17, MapGenerator.mapSize.y * 17);

        int x = Random.Range(-mapSize.x, mapSize.x);
        int y = Random.Range(-mapSize.y, mapSize.y);
        Vector3Int randomPosition = new(x, y, 0);

        if (IsObjectAtPosition(randomPosition)) return GetPosition();

        return waterTilemap.GetCellCenterWorld(randomPosition);
    }

    private bool IsObjectAtPosition(Vector3Int randomPosition)
    {
        Collider2D collider = Physics2D.OverlapBox(new Vector2(randomPosition.x, randomPosition.y), Vector2.one, 0);

        if (collider != null)
        {
            if (waterTilemap.GetTile(randomPosition) != null) return true;
            if (!collider.CompareTag("Chunk")) return true;
        }

        return false;
    }
}