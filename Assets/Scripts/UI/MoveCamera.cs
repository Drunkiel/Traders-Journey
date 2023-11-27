using Cinemachine;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 difference;
    public CinemachineVirtualCamera virtualCamera;
    public Transform objectToMove;
    public Transform grid;

    private bool drag;

    public float zoomSpeed = 5.0f;
    public float minZoom = 2.0f;    
    public float maxZoom = 20.0f;   

    private void LateUpdate()
    {
        if (GameController.isGamePaused) return;
/*        if (BuildingSystem.inBuildingMode) return;*/

        Move();
        Zoom();
    }

    private void Move()
    {
        if (Input.GetMouseButton(1))
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - objectToMove.position;
            if (!drag)
            {
                drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else drag = false;

        if (drag)
        {
            Vector3 newPos = origin - difference;
            objectToMove.position = Vector3.Lerp(objectToMove.position, newPos, 0.1f);
            grid.position = SnapCoordinateToGrid(objectToMove.position);
        }
    }

    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            float newSize = virtualCamera.m_Lens.OrthographicSize - scroll * zoomSpeed;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }

    private Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPosition = BuildingSystem.instance.gridLayout.WorldToCell(position);

        return new Vector2(cellPosition.x, cellPosition.y);
    }
}
