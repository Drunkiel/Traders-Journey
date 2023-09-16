using UnityEngine;

public class MultiTriggerController : MonoBehaviour
{
    public bool isTriggered;
    public string[] objectsTag;

    void OnTriggerEnter2D(Collider2D collider)
    {
        for (int i = 0; i < objectsTag.Length; i++)
        {
            if (collider.CompareTag(objectsTag[i]))
            {
                isTriggered = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        for (int i = 0; i < objectsTag.Length; i++)
        {
            if (collider.CompareTag(objectsTag[i]))
            {
                isTriggered = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        for (int i = 0; i < objectsTag.Length; i++)
        {
            if (collider.CompareTag(objectsTag[i]))
            {
                isTriggered = false;
            }
        }
    }
}
