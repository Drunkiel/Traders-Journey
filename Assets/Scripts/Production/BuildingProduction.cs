using System;
using UnityEngine;

[Serializable]
public class Production
{
    public Resources resources;
    public int quantity;
    public int productionTime;
}

public class BuildingProduction : MonoBehaviour
{
    public int producingDays;
    private Production[] _productions;

    private void Start()
    {
        SetProductions();
        CycleController.instance.endDayEvent.AddListener(() => CheckIfProductionEnded());
    }

    private void SetProductions()
    {
        _productions = GetComponent<BuildingID>()._productions;
    }

    public void CheckIfProductionEnded()
    {
        producingDays++;
        if (producingDays == _productions[0].productionTime) EndProduction();
    }

    private void EndProduction()
    {
        for (int i = 0; i < _productions.Length; i++)
        {
            ResourcesData.instance.AddResources(_productions[i].resources, _productions[i].quantity);
        }
        producingDays = 0;
    }
}
