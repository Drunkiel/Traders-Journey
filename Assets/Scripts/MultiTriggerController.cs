using UnityEngine;
using System.Collections.Generic;

public class MultiTriggerController : MonoBehaviour
{
    public bool isTriggered;
    public string[] objectsTag;
    public HashSet<string> objectsTagsSet;

    void Start()
    {
        objectsTagsSet = new HashSet<string>(objectsTag);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CheckCollision(collider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        CheckCollision(collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        CheckCollision(collider, false);
    }

    void CheckCollision(Collider2D collider, bool enter = true)
    {
        string colliderTag = collider.tag;

        if (objectsTagsSet.Contains(colliderTag))
        {
            isTriggered = enter;
            return;
        }
    }
}
