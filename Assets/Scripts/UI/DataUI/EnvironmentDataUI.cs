using UnityEngine;

public class EnvironmentDataUI : DataUI
{
    [HideInInspector] public EnvironmentID _environmentID;

    public override void SetData()
    {
        _data.name = _environmentID.objectName;
        _data.size = _environmentID.size;
        _data.prices = _environmentID.prices;
    }
}
