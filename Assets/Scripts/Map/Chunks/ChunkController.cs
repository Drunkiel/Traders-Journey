using UnityEngine;

public class ChunkController : MonoBehaviour
{
    public static Vector2Int singleChunkSize = new Vector2Int(10, 10);

    [SerializeField] private GameObject chunkPrefab;

    private void Start()
    {
        GenerateChunks();
    }

    private void GenerateChunks()
    {
        for (int i = -MapGenerator.mapSize.x; i < MapGenerator.mapSize.x; i++)
        {
            for (int j = -MapGenerator.mapSize.y; j < MapGenerator.mapSize.y; j++)
            {
                Instantiate(chunkPrefab, new Vector3(i * 20, j * 20, 0), Quaternion.identity);
            }
        }
    }
}
