using UnityEngine;

public class PlacableObject : MonoBehaviour
{
    public bool isPlaced;
    public Vector2Int size;
    public Vector2[] vertices;
    public GameObject border;

    public string[] objectsTag;

    private void Awake()
    {
        GetColliderVertexPositionLocal();
        CalculateSizeInCells();
    }

    public virtual void Place()
    {
        Destroy(GetComponent<ObjectDrag>());
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        BuildingSystem.instance._objectToPlace = null;
        GetComponent<Animator>().SetTrigger("Spawn");

        isPlaced = true;
    }

    public virtual void Move()
    {
        if (BuildingSystem.inBuildingMode) return;
        BuildingSystem.inBuildingMode = true;

        isPlaced = false;
        BuildingSystem.instance._objectToPlace = this;
        BuildingSystem.instance.OpenUI(false);

        gameObject.AddComponent<ObjectDrag>();
        Instantiate(border, transform);
    }

    private void GetColliderVertexPositionLocal()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        vertices = new Vector2[4];
        vertices[0] = collider.offset + new Vector2(-collider.size.x, -collider.size.y) * 0.5f;
        vertices[1] = collider.offset + new Vector2(collider.size.x, -collider.size.y) * 0.5f;
        vertices[2] = collider.offset + new Vector2(collider.size.x, -collider.size.y) * 0.5f;
        vertices[3] = collider.offset + new Vector2(-collider.size.x, -collider.size.y) * 0.5f;
    }

    private void CalculateSizeInCells()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        size = new Vector2Int(Mathf.CeilToInt(collider.size.x), Mathf.CeilToInt(collider.size.y));
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(vertices[0]);
    }
}