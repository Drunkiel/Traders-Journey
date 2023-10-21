using System;
using UnityEngine;

[Serializable]
public class Price
{
    public Resources resources;
    public int quantity;
}

public class BuildingID : MonoBehaviour
{
    public string buildingName;
    public Sprite[] buildingSprites;
    public bool onlyOne;
    public Vector2 size;
    public int buildingLevel;
    public Price[] _prices;
    public Production[] _productions;
}
