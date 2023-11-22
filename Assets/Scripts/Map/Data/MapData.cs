using UnityEngine;

[CreateAssetMenu(menuName = "Map generator/Map data")]
public class MapData : ScriptableObject
{
    public BuildingData _buildingData;
    public EnvironmentData _environmentData;
}
