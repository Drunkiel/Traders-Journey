using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private GameObject buildingUI;
    [SerializeField] private BuildingUI _buildingUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) CheckIfClickedSomething();
    }

    public void CheckIfClickedSomething()
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
                _buildingUI.UpdateData(_buildingID);
            }

            if (hit.collider.TryGetComponent(out EnvironmentID _environmentID))
            {
                print("test");
            }
        }
    }
}
