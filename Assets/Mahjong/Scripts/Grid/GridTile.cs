using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public bool IsEmpty => _pieceLinked == null;
    public MahjongPiece PieceLinked => _pieceLinked;

    private MahjongPiece _pieceLinked;
    private GridManager _gridManager;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LinkPiece(MahjongPiece piece)
    {
        _pieceLinked = piece;
    }

    public void LinkToManager(GridManager manager)
    {
        _gridManager = manager;
    }

}
