using System.Collections.Generic;
using UnityEngine;

public class MultiBuildingController : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    private Vector3 currentPosition;

    public bool isStartPositionPlaced;
    public bool isEndPositionPlaced;

    public List<Vector2> bestPositions = new List<Vector2>();
    Vector2Int[] possibleMoves = new Vector2Int[4] { new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, 0) }; //top, left, bottom, right

    public void MakePath()
    {
        currentPosition = startPosition;
        bestPositions.Clear();

        bestPositions.Add(currentPosition);
        while (currentPosition != endPosition)
        {
            Vector2 bestMove = GetBestMoves(currentPosition);
            currentPosition = BuildingSystem.instance.SnapCoordinateToGrid(new Vector3(currentPosition.x + bestMove.x, currentPosition.y + bestMove.y));
            bestPositions.Add(currentPosition);
        }
    }

    private Vector2Int GetBestMoves(Vector3 start)
    {
        if (start.x < endPosition.x) return possibleMoves[3];
        if (start.x > endPosition.x) return possibleMoves[1];
        if (start.y < endPosition.y) return possibleMoves[0];
        if (start.y > endPosition.y) return possibleMoves[2];

        return Vector2Int.zero;
    }
}
