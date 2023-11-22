using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Map generator/Environment data")]
public class EnvironmentData : ScriptableObject
{
    public Tile[] groundTiles;
    public Tile[] waterTiles;
    public GameObject[] treePrefabs;
    public GameObject[] rockPrefabs;
}
