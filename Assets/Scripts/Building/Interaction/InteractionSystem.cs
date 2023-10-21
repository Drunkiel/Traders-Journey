using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private GameObject buildingUI;
    [SerializeField] private BuildingUI _buildingUI;
    [SerializeField] private EnvironmentDataUI _environmentUI;

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
                _environmentUI.gameObject.SetActive(true);
                _environmentUI.transform.position = hit.collider.transform.position;
                _environmentUI._environmentID = _environmentID;
                _environmentUI.UpdateData();
            }
        }
    }
}
