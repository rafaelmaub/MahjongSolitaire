using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public bool available;
    private GridManager GridManager => GameManager.Instance.Grid;
    public bool IsEmpty => _pieceLinked == null;
    public MahjongPiece PieceLinked => _pieceLinked;

    private MahjongPiece _pieceLinked;

    private Vector3Int _coordinates;

    private void Update()
    {
        available = IsTilePlayable();
    }

    public void ResetTile()
    {
        if(_pieceLinked)
        {
            Destroy(_pieceLinked);
        }
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
