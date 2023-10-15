using System;
using UnityEngine;

[Serializable]
public class Price
{
    public Resources resources;
    public int quantity;
}

[Serializable]
public class Production
{
    public Resources resources;
    public int quantity;
    public int productionTime;
}

public class BuildingID : MonoBehaviour
{
    public string buildingName;
    public Sprite[] buildingSprites;
    public bool onlyOne;
    public Vector2 size;
    public int buildingLevel;
    public Price[] prices;
    public Production[] productions;
}
