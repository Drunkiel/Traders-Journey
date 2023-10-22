using System;

[Serializable]
public class Production
{
    public Resources resources;
    public int quantity;
    public int productionTime;
}

[Serializable]
public class BuildingProduction
{
    private int producingDays;
    [UnityEngine.HideInInspector] public Production[] _productions;

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
