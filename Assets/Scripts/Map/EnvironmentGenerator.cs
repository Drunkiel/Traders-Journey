using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentGenerator : MonoBehaviour
{
    private Tilemap waterTilemap;
    private int treesGenerated = 0;
    [SerializeField] private int maxTrees = 150;
    public Transform parent;

    private void Start()
    {
        waterTilemap = MapGenerator.instance.waterTilemap;
    }

    public void GenerateEnvironment()
    {
        waterTilemap = MapGenerator.instance.waterTilemap;
        StartCoroutine(GenerateEnvironment(0.1f));
    }

    private IEnumerator GenerateEnvironment(float interval)
    {
        GameObject[] prefabs = MapGenerator.instance._mapData.treePrefabs;

        while (treesGenerated <= maxTrees)
        {
            if (LakeGenerator.isLakeGenerated)
            {
                Vector3 randomPosition = GetPosition();
                if (randomPosition != Vector3.zero)
                {
                    Instantiate(prefabs[MapGenerator.instance.GetRandomTile(prefabs.Length)], randomPosition, Quaternion.identity, parent);
                    treesGenerated++;
                }
                else maxTrees--;
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private Vector3 GetPosition()
    {
        int x = Random.Range(-MapGenerator.mapSize.x, MapGenerator.mapSize.x);
        int y = Random.Range(-MapGenerator.mapSize.y, MapGenerator.mapSize.y);
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