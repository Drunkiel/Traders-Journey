using UnityEngine;

public class DestroyOn : MonoBehaviour
{
    PlacableObject _placableObject;
    MultiTriggerController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _placableObject = GetComponent<PlacableObject>();
        _controller = GetComponent<MultiTriggerController>();
    }

    // Update is called once per frame
    void Update()
    {
/*        if (_placableObject.isPlaced && _controller.isTriggered ) Destroy(gameObject);*/
    }
}
