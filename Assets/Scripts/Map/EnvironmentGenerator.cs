using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentGenerator : MonoBehaviour
{
    public static bool isEnviromentGenerated;
    private Tilemap waterTilemap;
    private int treesGenerated = 0;
    [SerializeField] private int maxTrees = 150;
    [SerializeField] private Transform parent;

    private void Start()
    {
        waterTilemap = MapGenerator.instance.waterTilemap;
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
                if (randomPosition != Vector3.zero)
                {
                    Instantiate(prefabs[_mapGenerator.GetRandomTile(prefabs.Length)], randomPosition, Quaternion.identity, parent);
                    treesGenerated++;
                }
                else maxTrees--;
            }
            yield return new WaitForSeconds(interval);
        }
        isEnviromentGenerated = true;
    }

    private Vector3 GetPosition()
    {
        Vector2Int chunkSize = ChunkController.singleChunkSize;

        int x = Random.Range(-chunkSize.x, chunkSize.x);
        int y = Random.Range(-chunkSize.y, chunkSize.y);
        Vector3Int randomPosition = new Vector3Int(x, y, 0);

        if (IsObjectAtPosition(randomPosition)) return Vector3.zero;

        return waterTilemap.GetCellCenterWorld(randomPosition);
    }

    private bool IsObjectAtPosition(Vector3Int randomPosition)
    {
        Collider2D colliders = Physics2D.OverlapBox(new Vector2(randomPosition.x, randomPosition.y), Vector2.one, 0);

        if (waterTilemap.GetTile(randomPosition) != null || colliders != null) return true;

        return false;
    }
}