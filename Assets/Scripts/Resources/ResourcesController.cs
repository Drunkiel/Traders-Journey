using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    private List<List<ResourceCard>> _resourceCards;

    private void Start()
    {
        _resourceCards = new() { _data.collectibleResources, _data.mineableResources };
    }

    public void AddResources(Resources resources, int quantity)
    {
        switch (resources)
        {
            case Resources.Coins:
                PlayerResources.coinsQuantity += quantity;
                RepetitiveActivities(resources, PlayerResources.coinsQuantity, quantity);
                break;
            case Resources.Wheat:
                PlayerResources.wheatQuantity += quantity;
                RepetitiveActivities(resources, PlayerResources.wheatQuantity, quantity);
                break;
            case Resources.Meat:
                PlayerResources.meatQuantity += quantity;
                RepetitiveActivities(resources, PlayerResources.meatQuantity, quantity);
                break;
            case Resources.Stone:
                PlayerResources.stoneQuantity += quantity;
                RepetitiveActivities(resources, PlayerResources.stoneQuantity, quantity);
                break;
            case Resources.Gold:
                PlayerResources.goldQuantity += quantity;
                RepetitiveActivities(resources, PlayerResources.goldQuantity, quantity);
                break;
            case Resources.Silver:
                PlayerResources.silverQuantity += quantity;
                RepetitiveActivities(resources, PlayerResources.silverQuantity, quantity);
                break;
            case Resources.Diamond:
                PlayerResources.diamondQuantity += quantity;
                RepetitiveActivities(resources, PlayerResources.diamondQuantity , quantity);
                break;
        }
    }

    private void RepetitiveActivities(Resources resources, int resourceQuantity, int quantity)
    {
        UpdateCard((int)resources, resources, resourceQuantity);
        SetAnimation((int)resources, resources, quantity);
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

    private void UpdateCard(int i, Resources resources, int quantity)
    {
        int a = CalculateA(i);

        if (a == 0) _data.coinsText.text = quantity.ToString();
        else
        {
            List<ResourceCard> cards = _resourceCards[a - 1];
            cards[FindResource(a - 1, resources)].quantity = PlayerResources.GetQuantity(resources);
        }
    }

    //a -> is which ResourceCard is where to look for a resource || resources -> is what script is looking for
    private int FindResource(int i, Resources resources)
    {
        int a = CalculateA(i);
        List<ResourceCard> cards = _resourceCards[a];

        for (int j = 0; j < cards.Count; j++)
        {
            if (cards[j].resource == resources) return j;
        }

        return 0;
    }

    private void SetAnimation(int i, Resources resources, int quantity)
    {
        string action = "+";
        Color newColor = BuildingSystem.instance.colors[0];
        if (quantity < 0)
        {
            action = "-";
            newColor = BuildingSystem.instance.colors[1];
        }

        int a = CalculateA(i);

        //Setting data
        if (a > 0) 
        {
            List<ResourceCard> cards = _resourceCards[a - 1];
            _data._showHides[a].transform.GetChild(1).GetComponent<Image>().sprite = cards[FindResource(a - 1, resources)].sprite; 
        }
        _data._showHides[a].GetComponentInChildren<TMP_Text>().color = new(newColor.r, newColor.g, newColor.b, 255);
        _data._showHides[a].GetComponentInChildren<TMP_Text>().text = action + Mathf.Abs(quantity);
        _data._showHides[a].ShowHideAnimation();
    }

    private int CalculateA(int i)
    {
        if (i > 0 && i <= _data.collectibleResources.Count) return 1;
        if (i > _data.collectibleResources.Count && i >= _data.collectibleResources.Count + _data.mineableResources.Count) return 2;

        return 0;
    }

    public void ControllerBTN(int i)
    {
        _resourcesUI.FillContent(_resourceCards[i]);
    }
}
