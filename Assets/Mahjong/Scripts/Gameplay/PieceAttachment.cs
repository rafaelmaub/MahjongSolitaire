using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceAttachment : MonoBehaviour
{
    public MahjongPiece Piece => piece;
    [SerializeField] protected MahjongPiece piece;


    protected virtual void Awake()
    {
        if(!piece)
        {
            piece = GetComponent<MahjongPiece>();
        }
    }
}
