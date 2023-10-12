using UnityEngine;

public class LakeGenerator : MonoBehaviour
{
    public static bool isLakeGenerated;
    public GameObject walkerPrefab;
    private WalkerController _walkerController;

    public void GenerateLake()
    {
        SpawnWalker();
        StartCoroutine(_walkerController.RepeatMoveWalker(GameController.isDevMode ? 0 : 0.25f));
    }

    private void SpawnWalker()
    {
        GameObject newWalker = Instantiate(walkerPrefab);
        _walkerController = newWalker.GetComponent<WalkerController>();
        _walkerController.tilemap = MapGenerator.instance.waterTilemap;
        _walkerController.SetPosition();
    }
}
