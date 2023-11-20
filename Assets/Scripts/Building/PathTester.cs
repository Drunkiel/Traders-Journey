using System.Collections.Generic;
using UnityEngine;

public class PathTester : MonoBehaviour
{
    public List<MultiTriggerController> _controllers = new List<MultiTriggerController>();
    [SerializeField] private GameObject prefab;
    private BuildingID _buildingID;

    private void Start()
    {
        _buildingID = BuildingSystem.instance._objectToPlace.GetComponent<BuildingID>();
        BuildingSystem.instance._pathTester = this;
        SpawnTesters();
    }

    private void Update()
    {
        if (_buildingID != null) transform.localPosition = _buildingID.transform.position;
        else Destroy(transform.gameObject);
    }

    private void SpawnTesters()
    {
        _controllers.Clear();
        float getX()
        {
            if (_buildingID.size.x == 1) return 0;
            else return 0.5f;
        }

        float getY()
        {
            if (_buildingID.size.y == 1) return 0;
            else return 0.5f;
        }

        for (int i = 0; i < _buildingID.size.x; i++)
        {
            GameObject newTester = Instantiate(prefab, transform);
            float x = getX();
            float y = getY();
            newTester.transform.localPosition = new Vector2(i > 0 ? x : -x, -1 + -y);
            _controllers.Add(newTester.GetComponent<MultiTriggerController>());
        }
    }

    public bool CheckForPath()
    {
        bool allTriggered = true;

        foreach (MultiTriggerController _controller in _controllers)
        {
            if (!_controller.isTriggered)
            {
                allTriggered = false;
                _controller.GetComponent<SpriteRenderer>().color = BuildingSystem.instance.colors[1];
            }
            else
            {
                _controller.GetComponent<SpriteRenderer>().color = BuildingSystem.instance.colors[0];
            }
        }

        return allTriggered;
    }

}
