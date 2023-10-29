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
                populationBox._showHide.ShowHideAnimation();
                populationBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "+" + quantity;
                PlayerResources.populationQuantity += quantity;
                break;
            case Resources.Coins:
                coinsBox._showHide.ShowHideAnimation();
                coinsBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "+" + quantity;
                PlayerResources.coinsQuantity += quantity;
                break;
            case Resources.Supplies:
                suppliesBox._showHide.ShowHideAnimation();
                suppliesBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "+" + quantity;
                PlayerResources.suppliesQuantity += quantity;
                break;
            case Resources.Wheat:
                wheatBox._showHide.ShowHideAnimation();
                wheatBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "+" + quantity;
                PlayerResources.wheatQuantity += quantity;
                break;
            case Resources.Meat:
                meatBox._showHide.ShowHideAnimation();
                meatBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "+" + quantity;
                PlayerResources.meatQuantity += quantity;
                break;
            case Resources.Stone:
                stoneBox._showHide.ShowHideAnimation();
                stoneBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "+" + quantity;
                PlayerResources.stoneQuantity += quantity;
                break;
            case Resources.Gold:
                goldBox._showHide.ShowHideAnimation();
                goldBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "+" + quantity;
                PlayerResources.goldQuantity += quantity;
                break;
            case Resources.Silver:
                silverBox._showHide.ShowHideAnimation();
                silverBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "+" + quantity;
                PlayerResources.silverQuantity += quantity;
                break;
            case Resources.Diamond:
                diamondBox._showHide.ShowHideAnimation();
                diamondBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "+" + quantity;
                PlayerResources.diamondQuantity += quantity;
                break;
        }
        UpdateResourcesPanel();
    }

    public void RemoveResources(Price[] prices)
    {
        if (CheckIfAllResources(prices))
        {
            for (int i = 0; i < prices.Length; i++)
            {
                switch (prices[i].resources)
                {
                    case Resources.Population:
                        populationBox._showHide.ShowHideAnimation();
                        populationBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "-" + prices[i].quantity; 
                        PlayerResources.populationQuantity -= prices[i].quantity;
                        break;
                    case Resources.Coins:
                        coinsBox._showHide.ShowHideAnimation();
                        coinsBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "-" + prices[i].quantity;
                        PlayerResources.coinsQuantity -= prices[i].quantity;
                        break;
                    case Resources.Supplies:
                        suppliesBox._showHide.ShowHideAnimation();
                        suppliesBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "-" + prices[i].quantity;
                        PlayerResources.suppliesQuantity -= prices[i].quantity;
                        break;
                    case Resources.Wheat:
                        wheatBox._showHide.ShowHideAnimation();
                        wheatBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "-" + prices[i].quantity;
                        PlayerResources.wheatQuantity -= prices[i].quantity;
                        break;
                    case Resources.Meat:
                        meatBox._showHide.ShowHideAnimation();
                        meatBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "-" + prices[i].quantity;
                        PlayerResources.meatQuantity -= prices[i].quantity;
                        break;
                    case Resources.Stone:
                        stoneBox._showHide.ShowHideAnimation();
                        stoneBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "-" + prices[i].quantity;
                        PlayerResources.stoneQuantity -= prices[i].quantity;
                        break;
                    case Resources.Gold:
                        goldBox._showHide.ShowHideAnimation();
                        goldBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "-" + prices[i].quantity;
                        PlayerResources.goldQuantity -= prices[i].quantity;
                        break;
                    case Resources.Silver:
                        silverBox._showHide.ShowHideAnimation();
                        silverBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "-" + prices[i].quantity;
                        PlayerResources.silverQuantity -= prices[i].quantity;
                        break;
                    case Resources.Diamond:
                        diamondBox._showHide.ShowHideAnimation();
                        diamondBox._showHide.transform.GetChild(1).GetComponent<TMP_Text>().text = "-" + prices[i].quantity;
                        PlayerResources.diamondQuantity -= prices[i].quantity;
                        break;
                }
            }
        }
        else print("No materials");
        UpdateResourcesPanel();
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

    private bool CheckIfAllResources(Price[] prices)
    {
        for (int i = 0; i < prices.Length; i++)
        {
            switch (prices[i].resources)
            {
                case Resources.Population:
                    if (PlayerResources.populationQuantity - prices[i].quantity < 0) { print(PlayerResources.populationQuantity - prices[i].quantity); return false; }
                    break;
                case Resources.Coins:
                    if (PlayerResources.coinsQuantity - prices[i].quantity < 0) { print(PlayerResources.coinsQuantity - prices[i].quantity); return false; }
                    break;
                case Resources.Supplies:
                    if (PlayerResources.suppliesQuantity - prices[i].quantity < 0) { print(PlayerResources.suppliesQuantity - prices[i].quantity); return false; }
                    break;
                case Resources.Wheat:
                    if (PlayerResources.wheatQuantity - prices[i].quantity < 0) { print(PlayerResources.wheatQuantity - prices[i].quantity); return false; }
                    break;
                case Resources.Meat:
                    if (PlayerResources.meatQuantity - prices[i].quantity < 0) { print(PlayerResources.meatQuantity - prices[i].quantity); return false; }
                    break;
                case Resources.Stone:
                    if (PlayerResources.stoneQuantity - prices[i].quantity < 0) { print(PlayerResources.stoneQuantity - prices[i].quantity); return false; }
                    break;
                case Resources.Gold:
                    if (PlayerResources.goldQuantity - prices[i].quantity < 0) { print(PlayerResources.goldQuantity - prices[i].quantity); return false; }
                    break;
                case Resources.Silver:
                    if (PlayerResources.silverQuantity - prices[i].quantity < 0) { print(PlayerResources.silverQuantity - prices[i].quantity); return false; }
                    break;
                case Resources.Diamond:
                    if (PlayerResources.diamondQuantity - prices[i].quantity < 0) { print(PlayerResources.diamondQuantity - prices[i].quantity); return false; }
                    break;
            }
        }
        return true;
    }
}
