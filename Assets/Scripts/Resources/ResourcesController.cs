using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Resources
{
    Coins,
    Wheat,
    Meat,
    Stone,
    Gold,
    Silver,
    Diamond
}

public class ResourcesController : MonoBehaviour
{
    [SerializeField] private ResourcesData _data;
    [SerializeField] private ResourcesUI _resourcesUI;
    [SerializeField] private TMP_Text coinsText;
    private List<List<ResourceCard>> _resourceCards;

    private void Start()
    {
        _resourceCards = new() { _data.collectibleResources, _data.mineableResources };
        AddResources(Resources.Wheat, 59);
    }

    //!ADD TO REST VOID SETANIMATION
    public void AddResources(Resources resources, int quantity)
    {
        switch (resources)
        {
            case Resources.Coins:
                PlayerResources.coinsQuantity += quantity;
                SetAnimation((int)Resources.Coins, quantity);
                break;
            case Resources.Wheat:
                PlayerResources.wheatQuantity += quantity;
                SetAnimation((int)Resources.Wheat, quantity);
                break;
            case Resources.Meat:
                PlayerResources.meatQuantity += quantity;
                SetAnimation((int)Resources.Meat, quantity);
                break;
            case Resources.Stone:
                PlayerResources.stoneQuantity += quantity;
                SetAnimation((int)Resources.Stone, quantity);
                break;
            case Resources.Gold:
                PlayerResources.goldQuantity += quantity;
                SetAnimation((int)Resources.Gold, quantity);
                break;
            case Resources.Silver:
                PlayerResources.silverQuantity += quantity;
                SetAnimation((int)Resources.Silver, quantity);
                break;
            case Resources.Diamond:
                PlayerResources.diamondQuantity += quantity;
                SetAnimation((int)Resources.Diamond, quantity);
                break;
        }
    }

    public void RemoveResources(Resources resources, int quantity)
    {
        if (!CheckForResources(resources, quantity))
        {
            print("Not enought resources: " + resources);
            return;
        }

        quantity *= -1;
        AddResources(resources, quantity);
    }

    private bool CheckForResources(Resources resources, int quantity)
    {
        int availableResources = 0;

        switch (resources)
        {
            case Resources.Coins:
                availableResources = PlayerResources.coinsQuantity;
                break;
            case Resources.Wheat:
                availableResources = PlayerResources.wheatQuantity;
                break;
            case Resources.Meat:
                availableResources = PlayerResources.meatQuantity;
                break;
            case Resources.Stone:
                availableResources = PlayerResources.stoneQuantity;
                break;
            case Resources.Gold:
                availableResources = PlayerResources.goldQuantity;
                break;
            case Resources.Silver:
                availableResources = PlayerResources.silverQuantity;
                break;
            case Resources.Diamond:
                availableResources = PlayerResources.diamondQuantity;
                break;
        }

        return availableResources - quantity >= 0;
    }

    private void UpdateCard(int a)
    {
        switch (a)
        {
            case 0:
                
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    private void SetAnimation(int i, int quantity)
    {
        string action = "+";
        Color newColor = BuildingSystem.instance.colors[0];
        if (quantity < 0)
        {
            action = "-";
            newColor = BuildingSystem.instance.colors[1];
        }

        int a = 0;
        if (i > 0 && i <= _data.collectibleResources.Count) a = 1;
        if (i > _data.collectibleResources.Count && i >= _data.collectibleResources.Count + _data.mineableResources.Count) a = 2;

        //Setting data
        _data._showHides[a].GetComponentInChildren<TMP_Text>().color = new(newColor.r, newColor.g, newColor.b, 255);
        _data._showHides[a].GetComponentInChildren<TMP_Text>().text = action + Mathf.Abs(quantity);
        _data._showHides[a].ShowHideAnimation();
    }

    public void ControllerBTN(int i)
    {
        _resourcesUI.FillContent(_resourceCards[i]);
    }
}
