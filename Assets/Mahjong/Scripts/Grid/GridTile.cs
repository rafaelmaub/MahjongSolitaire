using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    private GridManager GridManager => GameManager.Instance.Grid;
    public bool IsEmpty => _pieceLinked == null;
    public MahjongPiece PieceLinked => _pieceLinked;

    private MahjongPiece _pieceLinked;

    private Vector3Int _coordinates;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpTile(Vector3Int coordinates)
    {
        _coordinates = coordinates;
    }
    public bool IsTilePlayable()
    {
        bool leftAvailable = GridManager.GetPreviousTile(_coordinates) ? GridManager.GetPreviousTile(_coordinates).IsEmpty : true;
        bool rightAvailable = GridManager.GetNextTile(_coordinates) ? GridManager.GetNextTile(_coordinates).IsEmpty : true;
        return rightAvailable || leftAvailable;
    }
    public void LinkPiece(MahjongPiece piece)
    {
        _pieceLinked = piece;
    }

}
