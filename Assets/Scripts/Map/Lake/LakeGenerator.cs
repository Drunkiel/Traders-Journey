using UnityEngine;

public class LakeGenerator : MonoBehaviour
{
    public static bool isLakeGenerated;
    public GameObject walkerPrefab;
    private WalkerController _walkerController;

    public void GenerateLake()
    {
        SpawnWalker();
        StartCoroutine(_walkerController.RepeatMoveWalker(0.25f));
    }

    private void SpawnWalker()
    {
        GameObject newWalker = Instantiate(walkerPrefab);
        _walkerController = newWalker.GetComponent<WalkerController>();
        _walkerController.tile = MapGenerator.instance._mapData.waterTiles[0];
        _walkerController.tilemap = MapGenerator.instance.waterTilemap;
        _walkerController.SetPosition();
    }
}
