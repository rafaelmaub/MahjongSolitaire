using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahjongPiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer graphics;

    private TileData _tileData;
    private GridTile _tile;

    private void Awake()
    {
        if(!graphics)
        {
            graphics = GetComponentInChildren<SpriteRenderer>();
        }

    }

    public void SetupPiece(TileData data)
    {
        _tileData = data;

        graphics.sprite = _tileData.SpriteVisual;

    }
    public void LinkToTileSlot(GridTile tile)
    {
        _tile = tile;
    }
}
