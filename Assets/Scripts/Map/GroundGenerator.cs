using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundGenerator : MonoBehaviour
{
    public static bool isGroundGenerated;

    public void GenerateGround()
    {
        StartCoroutine(GenerateGrassTiles(0.01f));
    }

    private IEnumerator GenerateGrassTiles(float interval)
    {
        isGroundGenerated = false;
        Tile[] tiles = MapGenerator.instance._mapData.groundTiles;

        for (int i = -MapGenerator.mapSize.y; i < MapGenerator.mapSize.y; i++)
        {
            for (int j = -MapGenerator.mapSize.x; j < MapGenerator.mapSize.x; j++)
            {
                MapGenerator.instance.groundTilemap.SetTile(new Vector3Int(j, i), tiles[MapGenerator.instance.GetRandomTile(tiles.Length)]);
                yield return new WaitForSeconds(interval);
            }
        }

        isGroundGenerated = true;
    }
}