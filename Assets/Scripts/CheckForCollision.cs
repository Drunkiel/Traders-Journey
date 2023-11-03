using UnityEngine;

public class CheckForCollision : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Building")) return;
        BuildingSystem.instance.actualChunk = GetComponent<SingleChunk>();
    }
}
