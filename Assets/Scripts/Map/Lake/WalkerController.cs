using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerController : MonoBehaviour
{
    public Tilemap tilemap;

    private Vector3Int firstTargetPosition;
    private Vector3Int secondTargetPosition;

    bool isTop = false;
    bool isLeft = false;
    public bool reachedCenter;
    public bool reachedEnd;

    Vector2Int moveDirection = new Vector2Int(0, 1);
    Vector2Int[] possibleMoves = new Vector2Int[4] { new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, 0) };

    public IEnumerator RepeatMoveWalker(float interval)
    {
        while (!reachedEnd)
        {
            MoveWalker();
            LakeGenerator.isLakeGenerated = reachedEnd;
            yield return new WaitForSeconds(interval);
        }
    }

    private void MoveWalker()
    {
        MapGenerator _mapGenerator = MapGenerator.instance;

        Vector2 bestMove = GetBestMove(WalkerTarget());

        transform.position += new Vector3(bestMove.x, bestMove.y);

        PlaceAdditionalTiles(transform.position, moveDirection, 1f);
        tilemap.SetTile(tilemap.WorldToCell(transform.position), _mapGenerator._mapData.waterTiles[_mapGenerator.GetRandomTile(_mapGenerator._mapData.waterTiles.Length)]);
    }

    public void SetPosition()
    {
        Vector2Int chunkSize = ChunkController.singleChunkSize;

        if (Random.Range(0, 2) == 0) isTop = true;
        if (Random.Range(0, 2) == 0) isLeft = true;

        Vector3Int startPosition = new Vector3Int(
                isLeft ? Random.Range(-chunkSize.x, -chunkSize.x / 2) :
                         Random.Range(chunkSize.x / 2, chunkSize.x),

                isTop ? Random.Range(chunkSize.y / 2, chunkSize.y) :
                        Random.Range(-chunkSize.y, -chunkSize.y / 2)
            );

        //Setting the end
        secondTargetPosition = new Vector3Int(-startPosition.x + Random.Range(-5, 5), -startPosition.y + Random.Range(-5, 5));
        //Setting center
        firstTargetPosition = new Vector3Int(
                (startPosition.x + secondTargetPosition.x) / 2 + Random.Range(-10, 10),
                (startPosition.y + secondTargetPosition.y) / 2 + Random.Range(-10, 10)
            );

        transform.position = tilemap.GetCellCenterWorld(startPosition);
    }

    private Vector3 WalkerTarget()
    {
        if (Vector3.Distance(transform.position, tilemap.GetCellCenterWorld(firstTargetPosition)) <= 2f) reachedCenter = true;
        if (Vector3.Distance(transform.position, tilemap.GetCellCenterWorld(secondTargetPosition)) <= 2f) reachedEnd = true;

        if (!reachedCenter) return tilemap.GetCellCenterWorld(firstTargetPosition);
        else return tilemap.GetCellCenterWorld(secondTargetPosition);
    }

    private Vector2 GetBestMove(Vector3 target)
    {
        Vector3Int[] testPositions = new Vector3Int[4];
        List<float> distances = new List<float>();
        int[] a = new int[4] { 0, 1, 2, 3 };

        // Get all positions and distances
        for (int i = 0; i < 4; i++)
        {
            testPositions[i] = new Vector3Int(Mathf.RoundToInt(transform.position.x) + possibleMoves[i].x, Mathf.RoundToInt(transform.position.y) + possibleMoves[i].y, 0);
            distances.Add(Vector3.Distance(tilemap.GetCellCenterWorld(testPositions[i]), target));
        }

        //Sort positions to find best one
        for (int i = 0; i < distances.Count; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < distances.Count; j++)
            {
                if (distances[j] < distances[minIndex]) minIndex = j;
            }

            (a[minIndex], a[i]) = (a[i], a[minIndex]);
            (distances[minIndex], distances[i]) = (distances[i], distances[minIndex]);
        }

        return possibleMoves[a[0]];
    }

    private void PlaceAdditionalTiles(Vector3 position, Vector2Int direction, float width)
    {
        MapGenerator _mapGenerator = MapGenerator.instance;

        Vector3Int cellPosition = tilemap.WorldToCell(position);

        float randomWidth = Random.Range(width - 1f, width + 1f);

        // Tworzy kafelki w kszta³cie rzeki
        for (int i = -Mathf.RoundToInt(randomWidth / 2); i <= Mathf.RoundToInt(randomWidth / 2); i++)
        {
            Vector3Int adjacentCell = cellPosition + new Vector3Int(i, direction.y, 0);
            tilemap.SetTile(tilemap.WorldToCell(tilemap.GetCellCenterWorld(adjacentCell)), _mapGenerator._mapData.waterTiles[_mapGenerator.GetRandomTile(_mapGenerator._mapData.waterTiles.Length)]);
        }
    }
}