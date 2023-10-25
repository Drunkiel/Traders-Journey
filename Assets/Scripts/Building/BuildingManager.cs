using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;
    public ShowHide _buildingPanel;
    public GameObject buildingUI;
    public GameObject environmentUI;

    private void Awake()
    {
        instance = this;
    }

    public void ActivateBuildingMode()
    {
        if (BuildingSystem.inBuildingMode) TurnOffBuildingMode();
        else BuildingSystem.inBuildingMode = true;
    }

    public void TurnOffBuildingMode()
    {
        //Checking if any UI or some modes are ON
        if (_buildingPanel.isShown) return;
        if (buildingUI.activeSelf) return;
        if (environmentUI.activeSelf) return;

        BuildingSystem.inBuildingMode = false;
    }
}
