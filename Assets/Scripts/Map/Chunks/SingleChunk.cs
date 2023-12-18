using UnityEngine;

public class SingleChunk : MonoBehaviour
{
    public int id;
    public bool isOwned;
    private GameObject chunkUI;

    private void Start()
    {
        chunkUI = transform.GetChild(0).gameObject;
/*        MapGenerator.instance._groundGenerator.GenerateGround(new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)));*/
    }

    private void Update()
    {
        if (!isOwned) UpdateChunkVisibility();
    }

    public void BuyChunk()
    {
        isOwned = true;
        ChunkController.idOfAllNotOwnedChunks.RemoveAt(id);
        ChunkController.idOfAllOwnedChunks.Add(id);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void UpdateChunkVisibility()
    {
        Vector2 cameraPosition = Camera.main.transform.position;
        float distance = Vector2.Distance(transform.position, cameraPosition);

        if (distance < 50) chunkUI.SetActive(BuildingSystem.inBuildingMode);
        else chunkUI.SetActive(false);
    }
}
