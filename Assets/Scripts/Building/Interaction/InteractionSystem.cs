using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private GameObject buildingUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) CheckIfClickedBuilding();
    }

    public void CheckIfClickedBuilding()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; 

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldMousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (BuildingSystem.inBuildingMode) return;

            if (hit.collider.TryGetComponent(out BuildingID _buildingID))
            {
                buildingUI.SetActive(true);
                
            }
        }
    }
}
