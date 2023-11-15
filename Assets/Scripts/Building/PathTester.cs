using System.Collections.Generic;
using UnityEngine;

public class PathTester : MonoBehaviour
{
    [SerializeField] private List<MultiTriggerController> _controllers = new List<MultiTriggerController>();
    [SerializeField] private GameObject prefab;
    private BuildingID _buildingID;

    private void Start()
    {
        _buildingID = GetComponent<BuildingID>();
        SpawnTesters();
    }

    private void SpawnTesters()
    {
        for (int i = 0; i < _buildingID.size.x; i++)
        {
            GameObject newTester = Instantiate(prefab, new Vector2(0, -1), Quaternion.identity, transform.parent);
            _controllers.Add(newTester.GetComponent<MultiTriggerController>());
        }
    }

    public bool CheckForPath()
    {
        foreach (MultiTriggerController _controller in _controllers)
        {
            if (_controller.isTriggered)
            {
                _controller.GetComponent<SpriteRenderer>().color = BuildingSystem.instance.colors[0];
                return true;
            }
            else _controller.GetComponent<SpriteRenderer>().color = BuildingSystem.instance.colors[1];
        }

        return false;
    }
}
