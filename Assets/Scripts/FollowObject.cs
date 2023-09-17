using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Vector2 offSet;
    public Transform objectToFollow;
    private bool isRotated;

    // Update is called once per frame
    void Update()
    {
        if (objectToFollow != null)
        {
            Follow();
            ControlOffSet();
        }
    }

    private void Follow()
    {
        transform.position = new Vector2(objectToFollow.position.x + offSet.x, objectToFollow.position.y + offSet.y);
    }

    private void ControlOffSet()
    {
        if (objectToFollow.position.y > 0 && !isRotated)
        {
            offSet *= -1;
            isRotated = true;
        }

        if (objectToFollow.position.y < 0 && isRotated)
        {
            offSet *= -1;
            isRotated = false;
        }
    }
}
