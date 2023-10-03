using UnityEngine;
using UnityEngine.EventSystems;

public class ShowBuildingData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Transform buildingDataUI;

    private void Start()
    {
        buildingDataUI = GameObject.Find("BuildingDataUI").transform;
    }

    private void SetNewPosition()
    {
        if (transform.localPosition.x - 710 <= -900) buildingDataUI.localPosition = new Vector3(-860, transform.localPosition.y + 30);
        else buildingDataUI.localPosition = transform.localPosition + new Vector3(-710, 30);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetNewPosition();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buildingDataUI.localPosition = new Vector3(-1360, 0);
    }
}
