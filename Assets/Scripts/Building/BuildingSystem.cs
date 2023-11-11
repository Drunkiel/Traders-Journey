using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;
    public static bool inBuildingMode;

    public GridLayout gridLayout;

    public GameObject buildingState;
    public GameObject buildingGrid;
    [SerializeField] private Color32[] colors;

    [SerializeField] private GameObject UI;
    public PlacableObject _objectToPlace;
    [HideInInspector] public GameObject objectToPlaceCopy;
    public SingleChunk actualChunk;
    [SerializeField] private List<BuildingID> allBuildings = new List<BuildingID>();

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
                else if (Input.GetKeyDown(KeyCode.Escape)) DestroyButton();*/
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

        bool[] correctSize = SizeCorrection(); //0 is X || 1 is Y

        int addX = (correctSize[0] ? 1 : 0);
        int addY = (correctSize[1] ? 1 : 0);

        if (position.x > MapGenerator.mapSize.x * 20 ||
            position.x < -MapGenerator.mapSize.x * 20 + addX ||
            position.y > MapGenerator.mapSize.y * 20 ||
            position.y < -MapGenerator.mapSize.y * 20 + addY
            )
            return SnapCoordinateToGrid(Vector3.zero);

        return new Vector2(cellPosition.x + (correctSize[0] ? 0f : 0.5f), cellPosition.y + (correctSize[1] ? 0f : 0.5f));
    }

    public bool[] SizeCorrection()
    {
        bool additionalX = false;
        bool additionalY = false;

        if (_objectToPlace != null)
        {
            if (_objectToPlace.GetComponent<BuildingID>().size.x > 1) additionalX = true;
            if (_objectToPlace.GetComponent<BuildingID>().size.y > 1) additionalY = true;
        }

        return new bool[2] { additionalX, additionalY };
    }

    public void InitializeWithObject(GameObject prefab)
    {
        if (_objectToPlace != null) return;

        inBuildingMode = true;
        GameObject newObject = Instantiate(prefab);
        _objectToPlace = newObject.GetComponent<PlacableObject>();

        Vector3 position = SnapCoordinateToGrid(Camera.main.transform.position);
        newObject.transform.position = position;

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
        UI.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => MultiPlaceButton()  /*DefaultPlaceButton()*/);

        if (destroy) UI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => DestroyButton());
        else
        {
            Vector3 oldPosition = _objectToPlace.transform.position;
            UI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { _objectToPlace.transform.position = oldPosition; MultiPlaceButton(); /*DefaultPlaceButton()*/; });
        }
    }

    private void DefaultPlaceButton()
    {
        if (CanBePlaced())
        {
            if (BuildingValidation())
            {
                BuildingID _buildingID = _objectToPlace.GetComponent<BuildingID>();
                ResourcesData.instance.RemoveResources(_buildingID._prices);
                allBuildings.Add(_buildingID);
                _objectToPlace.Place();
            }
            else Destroy(_objectToPlace.gameObject);
        }
        else Destroy(_objectToPlace.gameObject);
        UI.SetActive(false);
        BuildingManager.instance.TurnOffBuildingMode();
    }

    private void MultiPlaceButton()
    {
        MultiBuildingController _controller = GetComponent<MultiBuildingController>();

        if (CanBePlaced())
        {
            if (!_controller.isStartPositionPlaced)
            {
                _controller.startPosition = SnapCoordinateToGrid(_objectToPlace.transform.position);
                _controller.isStartPositionPlaced = true;

                //Create replic of placed object
                _objectToPlace.Place();
                InitializeWithObject(objectToPlaceCopy);

                _objectToPlace.transform.position = SnapCoordinateToGrid(Camera.main.transform.position);
                return;
            }

            if (!_controller.isEndPositionPlaced)
            {
                _controller.endPosition = SnapCoordinateToGrid(_objectToPlace.transform.position);
                _controller.isEndPositionPlaced = true;

                BuildingID _buildingID = _objectToPlace.GetComponent<BuildingID>();
                ResourcesData.instance.RemoveResources(_buildingID._prices, 2);

                _objectToPlace.Place();
            }
        }
        else Destroy(_objectToPlace.gameObject);
        
        if (_controller.isEndPositionPlaced)
        {
            UI.SetActive(false);
            BuildingManager.instance.TurnOffBuildingMode();

            _controller.isStartPositionPlaced = false;
            _controller.isEndPositionPlaced = false;
        }
    }

    private bool BuildingValidation()
    {
        //Check if building can be placed once
        BuildingID _buildingID = _objectToPlace.GetComponent<BuildingID>();
        if (_buildingID.onlyOne)
        {
            if (CheckIfExists(_buildingID.buildingName))
            {
                DestroyButton();
                return false;
            }
        }

        return true;
    }

    private bool CheckIfExists(string buildingName)
    {
        foreach (var _buildingID in allBuildings)
        {
            if (_buildingID.buildingName.Equals(buildingName)) return true;
        }
        return false;
    }

    public void DestroyButton()
    {
        if (_objectToPlace != null) Destroy(_objectToPlace.gameObject);
        UI.SetActive(false);
        BuildingManager.instance.TurnOffBuildingMode();
    }

    private bool CanBePlaced(int quantity = 1)
    {
        //Check if building exists
        if (_objectToPlace == null) return false;

        //Other checks
        //Check if resources
        if (!ResourcesData.instance.CheckIfAllResources(_objectToPlace.GetComponent<BuildingID>()._prices))
        {
            print("Brak materia³ów");
            DestroyButton();
        }

        //Check if building is in owned chunk
        ChunkController.FindChunk(_objectToPlace.transform.position);
        if (ChunkController.idOfAllOwnedChunks.Count <= 0) return false;
        if (actualChunk == null) return false;
        if (!actualChunk.isOwned)
        {
            print("Chunk nie jest twoj¹ w³asnoœci¹");
            return false;
        }

        //If everything is okay
        return !_objectToPlace.transform.GetComponent<MultiTriggerController>().isTriggered;
    }

    private void ChangeMaterial(bool itCanBePlaced)
    {
        if (itCanBePlaced) _objectToPlace.transform.GetChild(_objectToPlace.transform.childCount - 1).GetComponent<SpriteRenderer>().color = colors[0];
        else _objectToPlace.transform.GetChild(_objectToPlace.transform.childCount - 1).GetComponent<SpriteRenderer>().color = colors[1];
    }
}