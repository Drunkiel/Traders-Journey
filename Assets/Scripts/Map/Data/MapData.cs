using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Map generator/Map data")]
public class MapData : ScriptableObject
{
    public Tile[] groundTiles;
    public Tile[] waterTiles;
    public GameObject[] treePrefabs;
    public GameObject[] rockPrefabs;

    public BuildingData _buildingData;
}
