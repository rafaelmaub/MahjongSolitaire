using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionOutline : PieceAttachment
{
    [SerializeField] private SpriteRenderer glow;
    protected override void Awake()
    {
        base.Awake();
        Piece.Interactions.OnPieceSelected += ShowHightLight;
        Piece.Interactions.OnPieceUnselected += HideHighlight;
    }

    private void ShowHightLight(MahjongPiece obj)
    {
        glow.enabled = true;
    }

    private void HideHighlight(MahjongPiece obj)
    {
        glow.enabled = false;
    }
}
