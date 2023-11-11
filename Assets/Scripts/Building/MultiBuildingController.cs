using System.Collections.Generic;
using UnityEngine;

public class MultiBuildingController : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;

    public bool isStartPositionPlaced;
    public bool isEndPositionPlaced;

    public List<Vector2> bestMoves = new List<Vector2>();
    Vector2Int[] possibleMoves = new Vector2Int[4] { new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, 0) };

    public void MakePath()
    {
        
    }

    private Vector3 GetBestMoves(Vector3 target)
    {
        return Vector3.down;
    }
}
