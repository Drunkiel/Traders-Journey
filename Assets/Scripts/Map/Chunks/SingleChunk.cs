using UnityEngine;

public class SingleChunk : MonoBehaviour
{
    public int id;
    public bool isOwned;

    public void BuyChunk()
    {
        isOwned = true;
        ChunkController.idOfAllNotOwnedChunks.RemoveAt(id);
        ChunkController.idOfAllOwnedChunks.Add(id);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
