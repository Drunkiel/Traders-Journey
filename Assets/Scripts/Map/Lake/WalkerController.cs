using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerController : MonoBehaviour
{
    public Tilemap tilemap;

    public List<Vector2Int> points = new List<Vector2Int>();
    public bool reachedEnd;

    public void GenerateMap(Vector2Int size_map, int nb_steps)
    {
        MapGenerator _mapGenerator = MapGenerator.instance;
        EnvironmentData _environmentData = _mapGenerator._mapData._environmentData;

        // choose a random starting point
        Vector2Int curr_pos = new Vector2Int(Random.Range(-size_map.x, size_map.x), Random.Range(-size_map.y, size_map.y));

        // define allowed movements: left, up, right, down
        List<Vector2Int> allowed_movements = new List<Vector2Int>
        {
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down
        };

        // iterate on the number of steps and move around
        for (int id_step = 0; id_step < nb_steps; id_step++)
        {
            Vector2Int new_pos = curr_pos + allowed_movements[Random.Range(0, allowed_movements.Count)];
            curr_pos = new_pos;
            points.Add(new_pos);
            transform.position = new Vector3(new_pos.x, new_pos.y);

            Vector3Int tilePosition = tilemap.WorldToCell(transform.position);
            tilemap.SetTile(tilemap.WorldToCell(tilemap.GetCellCenterWorld(tilePosition)),
                _environmentData.waterTiles[_mapGenerator.GetRandomTile(_environmentData.waterTiles.Length)]);
        }

        return;
    }
}