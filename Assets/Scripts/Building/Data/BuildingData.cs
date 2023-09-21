using UnityEngine;

[CreateAssetMenu(menuName = "Map generator/Building data")]
public class BuildingData : ScriptableObject
{
    public GameObject[] castleBuildings;
    public GameObject[] houseBuildings;
    public GameObject[] traderBuildings;
    public GameObject[] productionBuildings;
    public GameObject[] armyBuildings;
}
