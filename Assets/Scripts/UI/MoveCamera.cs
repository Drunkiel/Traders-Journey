using Cinemachine;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 difference;
    public CinemachineVirtualCamera virtualCamera;

    private bool drag;

    public float zoomSpeed = 3.0f;
    public float minZoom = 2.0f;    
    public float maxZoom = 20.0f;   

    private void LateUpdate()
    {
        if (GameController.isGamePaused) return;
        if (BuildingSystem.inBuildingMode) return;

        Move();
        Zoom();
    }

    private void Move()
    {
        if (Input.GetMouseButton(1))
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
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
            transform.position = Vector3.Lerp(transform.position, newPos, 0.1f);
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
}
