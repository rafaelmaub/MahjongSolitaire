using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MaterialHighlighter))]
public class MahjongPiece : MonoBehaviour
{
    public SpriteRenderer Symbol => graphics;

    [SerializeField] private SpriteRenderer graphics;
    [SerializeField] private MaterialHighlighter highlighter;

    private TileData _tileData;
    private GridTile _tile;

    private void Awake()
    {
        if(!graphics)
        {
            graphics = GetComponentInChildren<SpriteRenderer>();
        }

        if(!highlighter)
        {
            highlighter = GetComponent<MaterialHighlighter>();

        }

        highlighter.InitializeHighLight(this);
    }

    public bool IsPlayable()
    {
        return _tile.IsTilePlayable();
    }

    public void SetupPiece(TileData data)
    {
        _tileData = data;

        if(graphics)
        {
            graphics.sprite = _tileData.SpriteVisual;
        }
        

    }
    public void LinkToTileSlot(GridTile tile)
    {
        _tile = tile;
    }
}
