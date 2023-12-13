using System.Collections.Generic;
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

    public void AddResources(Resources resources, int quantity)
    {
        switch (resources)
        {
            case Resources.Coins:
                PlayerResources.coinsQuantity += quantity;
                break;
            case Resources.Wheat:
                PlayerResources.wheatQuantity += quantity;
                break;
            case Resources.Meat:
                PlayerResources.meatQuantity += quantity;
                break;
            case Resources.Stone:
                PlayerResources.stoneQuantity += quantity;
                break;
            case Resources.Gold:
                PlayerResources.goldQuantity += quantity;
                break;
            case Resources.Silver:
                PlayerResources.silverQuantity += quantity;
                break;
            case Resources.Diamond:
                PlayerResources.diamondQuantity += quantity;
                break;
        }
    }

    public void ControllerBTN(int i)
    {
        List<List<ResourceCard>> resourceCards = new() { _data.collectibleResources, _data.mineableResources };
        _resourcesUI.FillContent(resourceCards[i]);
    }
}
