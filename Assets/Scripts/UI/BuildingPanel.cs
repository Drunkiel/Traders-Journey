using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanel : MonoBehaviour
{
    public Transform parent;
    public GameObject prefab;
    private BuildingData _buildingData;
    [SerializeField] private TMP_Text sectionNameText;
    [SerializeField] private int currentPage = 1;
    [SerializeField] private int allPages = 1;
    private int currentContent;
    private int maxCardsToPage = 8;

    public GameObject[] pageButtons;

    private void Start()
    {
        _buildingData = MapGenerator.instance._mapData._buildingData;
        FillContent(0);
    }

    public void FillContent(int i)
    {
        GameObject[] buildings()
        {
            currentContent = i;

            switch (i)
            {
                case 0:
                    sectionNameText.text = "Castle section";
                    return _buildingData.castleBuildings;
                case 1:
                    sectionNameText.text = "House section";
                    return _buildingData.houseBuildings;
                case 2:
                    sectionNameText.text = "Trader section";
                    return _buildingData.traderBuildings;
                case 3:
                    sectionNameText.text = "Production section";
                    return _buildingData.productionBuildings;
                case 4:
                    sectionNameText.text = "Army section";
                    return _buildingData.armyBuildings;
                case 5:
                    sectionNameText.text = "?";
                    return _buildingData.castleBuildings;
            }

            return null;
        }

        DestroyOldCards();
        SpawnNewCards(buildings());
    }

    private void SpawnNewCards(GameObject[] buildings)
    {
        allPages = Mathf.CeilToInt(buildings.Length / maxCardsToPage) + 1; //Idk why but this would not round the number to top so I need add 1 to make it work good :0
        if (currentPage > allPages) currentPage = 1;
        if (allPages <= 1)
        {
            for (int i = 0; i < pageButtons.Length; i++)
                pageButtons[i].GetComponent<Button>().interactable = false;
        }
        else
        {
            for (int i = 0; i < pageButtons.Length; i++)
                pageButtons[i].GetComponent<Button>().interactable = true;
        }

        int getLength()
        {
            if (buildings.Length <= maxCardsToPage) return buildings.Length;
            if (buildings.Length > maxCardsToPage)
            {
                int number = maxCardsToPage * currentPage - buildings.Length;
                return maxCardsToPage * currentPage - (currentPage == allPages ? number : 0);
            }

            return 0;
        }

        int length = getLength();

        for (int i = maxCardsToPage * currentPage - maxCardsToPage; i < length; i++)
        {
            BuildingID _buildingID = buildings[i].GetComponent<BuildingID>();

            //Spawn button
            GameObject newPrefab = Instantiate(prefab, parent);
            newPrefab.GetComponent<BuildingUI>().UpdateData(_buildingID);
            newPrefab.GetComponent<ShowBuildingData>()._buildingID = _buildingID;
            newPrefab.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingSystem.instance.InitializeWithObject(_buildingID.gameObject);
                BuildingSystem.instance.objectToPlaceCopy = _buildingID.gameObject;
            });
        }
    }

    private void DestroyOldCards()
    {
        if (parent.childCount == 0) return;

        List<GameObject> oldBuildings = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
            oldBuildings.Add(parent.GetChild(i).gameObject);

        for (int i = 0; i < parent.childCount; i++)
            Destroy(oldBuildings[i]);
    }

    public void PageController(int i)
    {
        currentPage += i;

        if (currentPage > allPages) currentPage = 1;
        if (currentPage <= 0) currentPage = allPages;

        FillContent(currentContent);
    }
}
