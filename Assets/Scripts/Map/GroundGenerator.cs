using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundGenerator : MonoBehaviour
{
    public static bool isGroundGenerated;

    public void GenerateGround()
    {
        StartCoroutine(GenerateGrassTiles(0f));
    }

    private IEnumerator GenerateGrassTiles(float interval)
    {
        isGroundGenerated = false;
        Tile[] tiles = MapGenerator.instance._mapData.groundTiles;

        for (int i = ChunkController.singleChunkSize.y; i > -ChunkController.singleChunkSize.y; i--)
        {
            for (int j = -ChunkController.singleChunkSize.x; j < ChunkController.singleChunkSize.x; j++)
            {
                MapGenerator.instance.groundTilemap.SetTile(new Vector3Int(j, i - 1), tiles[MapGenerator.instance.GetRandomTile(tiles.Length)]);
                yield return new WaitForSeconds(interval);
            }
        }

        isGroundGenerated = true;
    }
}