using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanel : MonoBehaviour
{
    public Transform parent;
    public GameObject prefab;
    private BuildingData _buildingData;

    private void Start()
    {
        _buildingData = MapGenerator.instance._mapData._buildingData;
        FillContent(0);
    }

    public void FillContent(int i)
    {
        GameObject[] buildings()
        {
            switch (i)
            {
                case 0:
                    return _buildingData.castleBuildings;
                case 1:
                    return _buildingData.houseBuildings;
                case 2:
                    return _buildingData.traderBuildings;
                case 3:
                    return _buildingData.productionBuildings;
                case 4:
                    return _buildingData.armyBuildings;
                case 5:
                    return _buildingData.castleBuildings;
            }

            return null;    
        }

        DestroyOldCards();
        SpawnNewCards(buildings());
    }

    private void SpawnNewCards(GameObject[] buildings)
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            BuildingID _buildingID = buildings[i].GetComponent<BuildingID>();

            //Spawn button
            GameObject newPrefab = Instantiate(prefab, parent);
            newPrefab.transform.GetChild(0).GetComponent<Image>().sprite = _buildingID.buildingSprite;
            newPrefab.transform.GetChild(1).GetComponent<TMP_Text>().text = _buildingID.buildingName;
        }
    }

    private void DestroyOldCards()
    {
        if (parent.childCount == 0) return;

        List<GameObject> oldBuildings = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
        {
            oldBuildings.Add(parent.GetChild(i).gameObject);
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(oldBuildings[i]);
        }
    }
}
