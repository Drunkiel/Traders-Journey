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
}
