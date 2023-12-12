using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;
    public static bool inBuildingMode;

    public GridLayout gridLayout;

    public GameObject buildingState;
    public GameObject pathTester;
    public GameObject buildingGrid;
    public Color32[] colors;

    [SerializeField] private GameObject UI;
    public PlacableObject _objectToPlace;
    public PathTester _pathTester;
    [HideInInspector] public GameObject objectToPlaceCopy;
    public SingleChunk actualChunk;
    public List<BuildingID> _allBuildings = new();
    public List<BuildingID> _allPaths = new();

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!_objectToPlace) return;

        ChangeMaterial(_objectToPlace.canBePlaced);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _objectToPlace.transform.position = SnapCoordinateToGrid(GetMouseWorldPosition());
        }
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

        if (_objectToPlace.GetComponent<BuildingID>().needPath) Instantiate(pathTester);

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
        UI.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!_objectToPlace.GetComponent<BuildingID>().multiPlace) DefaultPlaceButton();
            else MultiPlaceButton();
        });

        if (destroy) UI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => DestroyButton());
        else
        {
            Vector3 oldPosition = _objectToPlace.transform.position;
            UI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
            {
                _objectToPlace.transform.position = oldPosition;
                if (!_objectToPlace.GetComponent<BuildingID>().multiPlace) DefaultPlaceButton();
                else MultiPlaceButton();
            });
        }
    }

    private void DefaultPlaceButton()
    {
        if (_objectToPlace.canBePlaced)
        {
            if (BuildingValidation())
            {
                BuildingID _buildingID = _objectToPlace.GetComponent<BuildingID>();
                //ResourcesData.instance.RemoveResources(_buildingID._prices);
                _allBuildings.Add(_buildingID);
                _objectToPlace.Place();
            }
            else DestroyButton();
        }
        else DestroyButton();

        DestroyButton();
    }

    private void MultiPlaceButton()
    {
        MultiBuildingController _controller = GetComponent<MultiBuildingController>();
        BuildingID _buildingID = objectToPlaceCopy.GetComponent<BuildingID>(); //This component is using a prefab to get data

        if (!_controller.isStartPositionPlaced)
        {
            _controller.startPosition = SnapCoordinateToGrid(_objectToPlace.transform.position);
            _controller.isStartPositionPlaced = true;

            if (!_objectToPlace.canBePlaced)
            {
                DestroyButton();
                return;
            }

            _objectToPlace.Place();
            //ResourcesData.instance.RemoveResources(_buildingID._prices);

            //Create replic of placed object
            InitializeWithObject(objectToPlaceCopy);
            _objectToPlace.transform.position = SnapCoordinateToGrid(_controller.startPosition);

            return;
        }

        if (!_controller.isEndPositionPlaced)
        {
            _controller.endPosition = SnapCoordinateToGrid(_objectToPlace.transform.position);
            _controller.isEndPositionPlaced = true;

            GameObject objectToDestroy = _objectToPlace.gameObject;
            _objectToPlace = null;
            Destroy(objectToDestroy);

            _controller.MakePath();
            StartCoroutine(StartPlacingObjects(_controller, _buildingID));
        }

        if (_controller.isEndPositionPlaced) DestroyButton();
    }

    IEnumerator StartPlacingObjects(MultiBuildingController _controller, BuildingID _buildingID)
    {
        for (int i = 0; i < _controller.bestPositions.Count; i++)
        {
            InitializeWithObject(objectToPlaceCopy);
            _objectToPlace.transform.position = _controller.bestPositions[i];
            yield return new WaitForSeconds(0.05f);

            if (_objectToPlace.canBePlaced)
            {
                _allPaths.Add(_objectToPlace.GetComponent<BuildingID>()); //This component is taken from the actual game object
                _objectToPlace.Place();
                //ResourcesData.instance.RemoveResources(_buildingID._prices);
            }
            else DestroyButton();
        }

        DestroyButton();
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
        foreach (var _buildingID in _allBuildings)
        {
            if (_buildingID.buildingName.Equals(buildingName)) return true;
        }
        return false;
    }

    public void DestroyButton()
    {
        if (_objectToPlace != null)
        {
            BuildingID _buildingID = _objectToPlace.GetComponent<BuildingID>();
            Destroy(_objectToPlace.gameObject);

            if (_buildingID.multiPlace)
            {
                MultiBuildingController _controller = GetComponent<MultiBuildingController>();
                _controller.isStartPositionPlaced = false;
                _controller.isEndPositionPlaced = false;
            }
        }

        UI.SetActive(false);
        BuildingManager.instance.TurnOffBuildingMode();
    }

    private void ChangeMaterial(bool itCanBePlaced)
    {
        if (itCanBePlaced) _objectToPlace.transform.GetChild(_objectToPlace.transform.childCount - 1).GetComponent<SpriteRenderer>().color = colors[0];
        else _objectToPlace.transform.GetChild(_objectToPlace.transform.childCount - 1).GetComponent<SpriteRenderer>().color = colors[1];
    }
}