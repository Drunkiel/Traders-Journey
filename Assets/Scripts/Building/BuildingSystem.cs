using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;
    public static bool inBuildingMode;

    [SerializeField] private GridLayout gridLayout;

    [SerializeField] private GameObject[] buildingsPrefabs;
    public GameObject buildingState;
    [SerializeField] private Color32[] colors;

    [SerializeField] private GameObject UI;
    /*    public BuildingUI _buildingUI;*/
    public PlacableObject _objectToPlace;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!_objectToPlace) return;

        ChangeMaterial(CanBePlaced());

        /*        if (Input.GetKeyDown(KeyCode.Space))
                {
                    PlaceButton();
                }
                else if (Input.GetKeyDown(KeyCode.Escape)) Destroy(_objectToPlace.gameObject);*/
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        return worldPosition;
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPosition = gridLayout.WorldToCell(position);

        if (position.x > MapGenerator.mapSize.x || position.x < -MapGenerator.mapSize.x || position.y > MapGenerator.mapSize.y || position.y < -MapGenerator.mapSize.y)
            return SnapCoordinateToGrid(Vector3.zero);

        if (_objectToPlace != null)
        {
            if (_objectToPlace.size.x == 1 && _objectToPlace.size.y == 1) return new Vector2(cellPosition.x + 0.5f, cellPosition.y + 0.5f);
        }

        return cellPosition;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        if (inBuildingMode) return;

        inBuildingMode = true;
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject newObject = Instantiate(prefab, position, Quaternion.identity);
        _objectToPlace = newObject.GetComponent<PlacableObject>();
        Instantiate(buildingState, newObject.transform);
        newObject.AddComponent<ObjectDrag>();

        OpenUI(true);
    }

    public void OpenUI(bool destroy)
    {
        //Set object variables
        FollowObject _followObject = UI.GetComponent<FollowObject>();
        _followObject.objectToFollow = _objectToPlace.transform;


        //UI
        UI.SetActive(true);

        //Removing listeners
        UI.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        UI.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();

        //Adding new listeners
        UI.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => PlaceButton());

        if (destroy) UI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { Destroy(_objectToPlace.gameObject); UI.SetActive(false); inBuildingMode = false; });
        else
        {
            Vector3 oldPosition = _objectToPlace.transform.position;
            UI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { _objectToPlace.transform.position = oldPosition; PlaceButton(); });
        }
    }

    private void PlaceButton()
    {
        if (CanBePlaced()) _objectToPlace.Place();
        else Destroy(_objectToPlace.gameObject);
        UI.SetActive(false);
        inBuildingMode = false;
    }

    private bool CanBePlaced()
    {
        if (_objectToPlace == null) return false;
        return !_objectToPlace.transform.GetComponent<MultiTriggerController>().isTriggered;
    }

    private void ChangeMaterial(bool itCanBePlaced)
    {
        if (itCanBePlaced) _objectToPlace.transform.GetChild(_objectToPlace.transform.childCount - 1).GetComponent<SpriteRenderer>().color = colors[0];
        else _objectToPlace.transform.GetChild(_objectToPlace.transform.childCount - 1).GetComponent<SpriteRenderer>().color = colors[1];
    }
}