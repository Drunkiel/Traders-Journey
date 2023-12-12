using UnityEngine;

public class PlacableObject : MonoBehaviour
{
    public bool isPlaced;
    public Vector2[] vertices;
    public GameObject border;

    public bool canBePlaced;

    public string[] objectsTag;

    private void Awake()
    {
        GetColliderVertexPositionLocal();
    }

    private void Start()
    {
        canBePlaced = CanBePlaced();
    }

    private void Update()
    {
        if (!isPlaced) canBePlaced = CanBePlaced();
    }

    public virtual void Place()
    {
        BuildingSystem _buildingSystem = BuildingSystem.instance;

        Destroy(GetComponent<ObjectDrag>());
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        _buildingSystem._objectToPlace = null;

        if (_buildingSystem._pathTester != null)
        {
            GameObject pathTester = _buildingSystem._pathTester.gameObject;
            _buildingSystem._pathTester = null;
            Destroy(pathTester);
        }
        
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

    private bool CanBePlaced()
    {
        BuildingSystem _buildingSystem = BuildingSystem.instance;
        BuildingID _buildingID = GetComponent<BuildingID>();

        //Other checks
        //Check if resources
/*        if (!ResourcesData.instance.CheckIfAllResources(_buildingID._prices))
        {
            print("Brak materia³ów");
            return false;
        }*/

        //Check if building is in owned chunk
        ChunkController.FindChunk(transform.position);
        if (_buildingSystem.actualChunk == null) return false;
        if (!_buildingSystem.actualChunk.isOwned)
        {
            print("Chunk nie jest twoj¹ w³asnoœci¹");
            return false;
        }

        //Check for path
        if (_buildingID.needPath)
        {
            if (_buildingSystem._pathTester == null) return false;
            if (!_buildingSystem._pathTester.CheckForPath())
            {
                print("Brak œcie¿ki");
                return false;
            }
        }

        //If everything is okay
        return !transform.GetComponent<MultiTriggerController>().isTriggered;
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(vertices[0]);
    }
}