using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    public static Vector2Int singleChunkSize = new Vector2Int(11, 11);

    public static List<int> idOfAllNotOwnedChunks = new List<int>();
    public static List<int> idOfAllOwnedChunks = new List<int>();
    public static List<SingleChunk> allChunks = new List<SingleChunk>();

    [SerializeField] private Transform parent;
    [SerializeField] private GameObject chunkPrefab;

    private void Start()
    {
        GenerateChunks();
    }

    private void GenerateChunks()
    {
        int index = 0;
        for (int i = singleChunkSize.y - 1; i > -singleChunkSize.y; i--)
        {
            for (int j = -singleChunkSize.x + 1; j < singleChunkSize.x; j++)
            {
                //Create new chunk and rename it
                GameObject newChunk = Instantiate(chunkPrefab, new Vector3(j * 20, i * 20, 0), Quaternion.identity, parent);
                newChunk.name = "Chunk " + index;

                //Set new chunk to variables
                SingleChunk _singleChunk = newChunk.GetComponent<SingleChunk>();
                _singleChunk.id = index;
                allChunks.Add(_singleChunk);

                idOfAllNotOwnedChunks.Add(index);
                index++;
            }
        }
    }

    public static void FindChunk(Vector3 buildingPosition)
    {
        if (idOfAllOwnedChunks.Count <= 0)
        {
            BuildingSystem.instance.actualChunk = null;
            return;
        }

        BuildingSystem.instance.actualChunk = null;

        for (int i = 0; i < idOfAllOwnedChunks.Count; i++)
        {
            SingleChunk chunk = allChunks[idOfAllOwnedChunks[i]];

            Vector3 chunkCenter = chunk.transform.position;
            float chunkSize = MapGenerator.mapSize.x;

            bool[] correction = BuildingSystem.instance.SizeCorrection();

            //Calculate distances if in chunk
            float distanceX = Mathf.Abs(buildingPosition.x - chunkCenter.x);
            float distanceY = Mathf.Abs(buildingPosition.y - chunkCenter.y);

            //If finds stop the function
            if (distanceX <= chunkSize - (correction[0] ? 1 : 0) && distanceY <= chunkSize - (correction[1] ? 1 : 0))
            {
                BuildingSystem.instance.actualChunk = chunk;
                break; 
            }
        }
    }



}
