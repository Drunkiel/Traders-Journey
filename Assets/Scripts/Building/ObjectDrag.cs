using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offSet;

    private void OnMouseDown()
    {
        offSet = transform.position - BuildingSystem.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = BuildingSystem.GetMouseWorldPosition();
        Vector2 position = mousePosition + new Vector2(offSet.x, offSet.y);
        transform.position = BuildingSystem.instance.SnapCoordinateToGrid(position);
    }
}