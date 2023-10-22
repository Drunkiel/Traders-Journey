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
    public BuildingProduction _buildingProduction;

    private void Start()
    {
        if (_productions.Length > 0)
        {
            _buildingProduction._productions = _productions;
            CycleController.instance.endDayEvent.AddListener(() => _buildingProduction.CheckIfProductionEnded());
        }
    }
}
