using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Resources
{
    Population,
    Coins,
    Supplies,
    Wheat,
    Meat,
    Stone,
    Gold,
    Silver,
    Diamond
}

[System.Serializable]
public class SingleBox
{
    public Resources resourcesType;
    public Sprite sprite;
    public TMP_Text quantityText;
    public ShowHide _showHide;
}

public class ResourcesData : MonoBehaviour
{
    public static ResourcesData instance;
    public SingleBox populationBox;
    public SingleBox coinsBox;
    public SingleBox suppliesBox;
    public SingleBox wheatBox;
    public SingleBox meatBox;

    [Header("Materials")]
    [SerializeField] private ShowHide _materialsShowHide;
    public SingleBox materialsBox;
    public SingleBox stoneBox;
    public SingleBox goldBox;
    public SingleBox silverBox;
    public SingleBox diamondBox;

    void Awake()
    {
        instance = this;
    }

    public Sprite GetSprite(Resources resources)
    {
        List<SingleBox> list = new List<SingleBox>() { populationBox, coinsBox, suppliesBox, wheatBox, meatBox, stoneBox, goldBox, silverBox, diamondBox };

        for (int i = 0; i < 10; i++)
        {
            if (resources == list[i].resourcesType) return list[i].sprite;
        }

        return populationBox.sprite;
    }

    public void AddResources(Resources resources, int quantity)
    {
        switch (resources)
        {
            case Resources.Population:
                QuickUpdateResources(populationBox, quantity);
                PlayerResources.populationQuantity += quantity;
                break;
            case Resources.Coins:
                QuickUpdateResources(coinsBox, quantity);
                PlayerResources.coinsQuantity += quantity;
                break;
            case Resources.Supplies:
                QuickUpdateResources(suppliesBox, quantity);
                PlayerResources.suppliesQuantity += quantity;
                break;
            case Resources.Wheat:
                QuickUpdateResources(wheatBox, quantity);
                PlayerResources.wheatQuantity += quantity;
                break;
            case Resources.Meat:
                QuickUpdateResources(meatBox, quantity);
                PlayerResources.meatQuantity += quantity;
                break;

            //Mineable Materials
            case Resources.Stone:
                if (!_materialsShowHide.isShown)
                {
                    materialsBox = SetValues(stoneBox);
                    QuickUpdateResources(stoneBox, quantity, true);
                }
                else QuickUpdateResources(stoneBox, quantity);

                PlayerResources.stoneQuantity += quantity;
                break;
            case Resources.Gold:
                if (!_materialsShowHide.isShown)
                {
                    materialsBox = SetValues(goldBox);
                    QuickUpdateResources(goldBox, quantity, true);
                }
                else QuickUpdateResources(goldBox, quantity);
                PlayerResources.goldQuantity += quantity;
                break;
            case Resources.Silver:
                if (!_materialsShowHide.isShown)
                {
                    materialsBox = SetValues(silverBox);
                    QuickUpdateResources(silverBox, quantity, true);
                }
                else QuickUpdateResources(silverBox, quantity);
                PlayerResources.silverQuantity += quantity;
                break;
            case Resources.Diamond:
                if (!_materialsShowHide.isShown)
                {
                    materialsBox = SetValues(diamondBox);
                    QuickUpdateResources(diamondBox, quantity, true);
                }
                else QuickUpdateResources(diamondBox, quantity);
                PlayerResources.diamondQuantity += quantity;
                break;
        }

        UpdateResourcesPanel();
    }

    public void RemoveResources(Price[] prices, int quantity = 1)
    {
        for (int i = 0; i < prices.Length; i++)
        {
            switch (prices[i].resources)
            {
                case Resources.Population:
                    QuickUpdateResources(populationBox, -prices[i].quantity);
                    PlayerResources.populationQuantity -= prices[i].quantity * quantity;
                    break;
                case Resources.Coins:
                    QuickUpdateResources(coinsBox, -prices[i].quantity);
                    PlayerResources.coinsQuantity -= prices[i].quantity * quantity;
                    break;
                case Resources.Supplies:
                    QuickUpdateResources(suppliesBox, -prices[i].quantity);
                    PlayerResources.suppliesQuantity -= prices[i].quantity * quantity;
                    break;
                case Resources.Wheat:
                    QuickUpdateResources(wheatBox, -prices[i].quantity);
                    PlayerResources.wheatQuantity -= prices[i].quantity * quantity;
                    break;
                case Resources.Meat:
                    QuickUpdateResources(meatBox, -prices[i].quantity);
                    PlayerResources.meatQuantity -= prices[i].quantity * quantity;
                    break;

                //Mineable materials
                case Resources.Stone:
                    if (!_materialsShowHide.isShown)
                    {
                        materialsBox = SetValues(stoneBox);
                        QuickUpdateResources(stoneBox, -prices[i].quantity, true);
                    }
                    else QuickUpdateResources(stoneBox, -prices[i].quantity);
                    PlayerResources.stoneQuantity -= prices[i].quantity * quantity;
                    break;
                case Resources.Gold:
                    if (!_materialsShowHide.isShown)
                    {
                        materialsBox = SetValues(goldBox);
                        QuickUpdateResources(goldBox, -prices[i].quantity, true);
                    }
                    else QuickUpdateResources(goldBox, -prices[i].quantity);
                    PlayerResources.goldQuantity -= prices[i].quantity * quantity;
                    break;
                case Resources.Silver:
                    if (!_materialsShowHide.isShown)
                    {
                        materialsBox = SetValues(silverBox);
                        QuickUpdateResources(silverBox, -prices[i].quantity, true);
                    }
                    else QuickUpdateResources(silverBox, -prices[i].quantity);
                    PlayerResources.silverQuantity -= prices[i].quantity * quantity;
                    break;
                case Resources.Diamond:
                    if (!_materialsShowHide.isShown)
                    {
                        materialsBox = SetValues(diamondBox);
                        QuickUpdateResources(diamondBox, -prices[i].quantity, true);
                    }
                    else QuickUpdateResources(diamondBox, -prices[i].quantity);
                    PlayerResources.diamondQuantity -= prices[i].quantity * quantity;
                    break;
            }
        }

        UpdateResourcesPanel();
    }

    private void QuickUpdateResources(SingleBox resourceBox, int quantity, bool useMaterialBox = false)
    {
        bool test = quantity >= 0;
        string sign = test ? "+" : "-";

        SingleBox _box;
        if (useMaterialBox) _box = materialsBox;
        else _box = resourceBox;

        _box._showHide.ShowHideAnimation();
        Color defaultColor = test ? BuildingSystem.instance.colors[0] : BuildingSystem.instance.colors[1];
        Vector4 newColor = new(defaultColor.r, defaultColor.g, defaultColor.b, defaultColor.a + 0.5f);
        _box._showHide.transform.GetChild(1).GetComponent<TMP_Text>().color = newColor;
        _box._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = sign + Mathf.Abs(quantity);
    }

    private SingleBox SetValues(SingleBox resourceBox)
    {
        return new SingleBox()
        {
            resourcesType = resourceBox.resourcesType,
            sprite = resourceBox.sprite,
            quantityText = resourceBox.quantityText,
            _showHide = materialsBox._showHide
        };
    }

    public void UpdateResourcesPanel()
    {
        populationBox.quantityText.text = PlayerResources.populationQuantity.ToString();
        coinsBox.quantityText.text = PlayerResources.coinsQuantity.ToString();
        suppliesBox.quantityText.text = PlayerResources.suppliesQuantity.ToString();
        wheatBox.quantityText.text = PlayerResources.wheatQuantity.ToString();
        meatBox.quantityText.text = PlayerResources.meatQuantity.ToString();
        stoneBox.quantityText.text = PlayerResources.stoneQuantity.ToString();
        goldBox.quantityText.text = PlayerResources.goldQuantity.ToString();
        silverBox.quantityText.text = PlayerResources.silverQuantity.ToString();
        diamondBox.quantityText.text = PlayerResources.diamondQuantity.ToString();
    }

    public bool CheckIfAllResources(Price[] prices, int quantity = 1)
    {
        for (int i = 0; i < prices.Length; i++)
        {
            switch (prices[i].resources)
            {
                case Resources.Population:
                    if (PlayerResources.populationQuantity - prices[i].quantity * quantity < 0) { print(PlayerResources.populationQuantity - prices[i].quantity * quantity); return false; }
                    break;
                case Resources.Coins:
                    if (PlayerResources.coinsQuantity - prices[i].quantity * quantity < 0) { print(PlayerResources.coinsQuantity - prices[i].quantity * quantity); return false; }
                    break;
                case Resources.Supplies:
                    if (PlayerResources.suppliesQuantity - prices[i].quantity * quantity < 0) { print(PlayerResources.suppliesQuantity - prices[i].quantity * quantity); return false; }
                    break;
                case Resources.Wheat:
                    if (PlayerResources.wheatQuantity - prices[i].quantity * quantity < 0) { print(PlayerResources.wheatQuantity - prices[i].quantity * quantity); return false; }
                    break;
                case Resources.Meat:
                    if (PlayerResources.meatQuantity - prices[i].quantity * quantity < 0) { print(PlayerResources.meatQuantity - prices[i].quantity * quantity); return false; }
                    break;
                case Resources.Stone:
                    if (PlayerResources.stoneQuantity - prices[i].quantity * quantity < 0) { print(PlayerResources.stoneQuantity - prices[i].quantity * quantity); return false; }
                    break;
                case Resources.Gold:
                    if (PlayerResources.goldQuantity - prices[i].quantity * quantity < 0) { print(PlayerResources.goldQuantity - prices[i].quantity * quantity); return false; }
                    break;
                case Resources.Silver:
                    if (PlayerResources.silverQuantity - prices[i].quantity * quantity < 0) { print(PlayerResources.silverQuantity - prices[i].quantity * quantity); return false; }
                    break;
                case Resources.Diamond:
                    if (PlayerResources.diamondQuantity - prices[i].quantity * quantity < 0) { print(PlayerResources.diamondQuantity - prices[i].quantity * quantity); return false; }
                    break;
            }
        }
        return true;
    }
}
