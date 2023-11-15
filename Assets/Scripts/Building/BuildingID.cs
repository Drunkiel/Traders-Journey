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
    [Header("Basic building informations")]
    public string buildingName;
    public Sprite[] buildingSprites;
    public bool onlyOne;
    public bool needPath = true;
    public bool multiPlace;
    public Vector2 size;
    public int buildingLevel;

    [Header("Cost and production of the building")]
    public Price[] _prices;
    public Production[] _productions;
    [SerializeField] private BuildingProduction _buildingProduction;

    private void Start()
    {
        if (_productions.Length > 0)
        {
            _buildingProduction._productions = _productions;
            CycleController.instance.endDayEvent.AddListener(() => _buildingProduction.CheckIfProductionEnded());
        }
    }
}
