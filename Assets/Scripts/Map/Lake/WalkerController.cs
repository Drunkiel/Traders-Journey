using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerController : MonoBehaviour
{
    public Tilemap tilemap;

    public List<Vector2Int> points = new();
    public bool reachedEnd;

    //Possible moves
    List<Vector2Int> possibleMoves = new()
    {
        Vector2Int.left,
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down
    };

    public void GenerateMap(Vector2Int size_map, int steps)
    {
        MapGenerator _mapGenerator = MapGenerator.instance;
        EnvironmentData _environmentData = _mapGenerator._mapData._environmentData;

        //Random spawn point
        Vector2Int curr_pos = new Vector2Int(Random.Range(-size_map.x, size_map.x), Random.Range(-size_map.y, size_map.y));

        // Random move and setting tiles
        for (int i = 0; i < steps; i++)
        {
            Vector2Int new_pos = curr_pos + possibleMoves[Random.Range(0, possibleMoves.Count)];
            curr_pos = new_pos;
            points.Add(new_pos);
            transform.position = new Vector3(new_pos.x, new_pos.y);

            Vector3Int tilePosition = tilemap.WorldToCell(transform.position);
            tilemap.SetTile(tilemap.WorldToCell(tilemap.GetCellCenterWorld(tilePosition)),
                _environmentData.waterTiles[_mapGenerator.GetRandomTile(_environmentData.waterTiles.Length)]);
        }

        Destroy(gameObject);
    }
}