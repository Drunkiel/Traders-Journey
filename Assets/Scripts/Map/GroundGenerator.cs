using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundGenerator : MonoBehaviour
{
    public static bool isGroundGenerated;

    public void GenerateGround(Vector3Int chunkCenter)
    {
        StartCoroutine(GenerateGrassTiles(0f, chunkCenter));
    }

    private IEnumerator GenerateGrassTiles(float interval, Vector3Int chunkCenter)
    {
        Vector2Int chunkSize = ChunkController.singleChunkSize;
        MapGenerator _mapGenerator = MapGenerator.instance;
        EnvironmentData _environmentData = _mapGenerator._mapData._environmentData;

        isGroundGenerated = false;
        Tile[] tiles = _environmentData.groundTiles;

        for (int i = chunkSize.y; i > -chunkSize.y; i--)
        {
            for (int j = -chunkSize.x; j < chunkSize.x; j++)
            {
                _mapGenerator.groundTilemap.SetTile(new Vector3Int(chunkCenter.x + j, chunkCenter.y + i - 1), tiles[_mapGenerator.GetRandomTile(tiles.Length)]);
                yield return new WaitForSeconds(interval);
            }
        }

        isGroundGenerated = true;
    }
}