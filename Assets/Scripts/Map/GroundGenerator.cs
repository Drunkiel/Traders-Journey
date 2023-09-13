using System.Collections;
using UnityEngine;

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

        for (int i = -MapGenerator.mapSize.y; i < MapGenerator.mapSize.y; i++)
        {
            for (int j = -MapGenerator.mapSize.x; j < MapGenerator.mapSize.x; j++)
            {
                MapGenerator.instance.groundTilemap.SetTile(new Vector3Int(j, i), MapGenerator.instance._mapData.groundTiles[Random.Range(0, 3)]);
                yield return new WaitForSeconds(interval);
            }
        }

        isGroundGenerated = true;
    }
}