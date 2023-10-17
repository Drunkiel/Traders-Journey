using UnityEngine;

public class BuildingDataUI : DataUI
{
    [HideInInspector] public BuildingID _buildingID;

    public override void SetData()
    {
        _data.name = _buildingID.buildingName;
        _data.sprites = _buildingID.buildingSprites;
        _data.size = _buildingID.size;
        _data.prices = _buildingID.prices;
        _data.productions = _buildingID.productions;
    }
}
