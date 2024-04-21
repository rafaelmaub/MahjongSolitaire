using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MahjongPiece : MonoBehaviour
{
    public bool Selected => _selected;
    public SpriteRenderer Symbol => graphics;
    public SpriteRenderer TileGlow => tileGlow;
    public PieceInteractor Interactions => _interactor;
    [SerializeField] private SpriteRenderer tileGlow;
    [SerializeField] private SpriteRenderer graphics;

    private TileData _tileData;
    private GridTile _tile;
    private bool _selected;
    private PieceInteractor _interactor;
    
    private void Awake()
    {
        _interactor = new PieceInteractor();
        if(!graphics)
        {
            graphics = GetComponentInChildren<SpriteRenderer>();
        }

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

    public void UnselectTile()
    {
        _selected = false;
        Interactions.Evt_OnPieceUnselected(this);
    }

    public void SelectTile()
    {
        if (_selected)
        {
            UnselectTile();
            return;
        }

        if (IsPlayable())
        {
            _selected = true;
            Interactions.Evt_OnPieceSelected(this);
        }
        else
        {

            //Effects here
            //Visual Feedback

        }
    }
}

public class PieceInteractor
{
    public event Action<MahjongPiece> OnPieceSelected;
    public event Action<MahjongPiece> OnPieceUnselected;
    public event Action OnPieceHoverEnter;
    public event Action OnPieceHoverExit;

    public void Evt_OnPieceSelected(MahjongPiece pc)
    {
        OnPieceSelected.Invoke(pc);
    }

    public void Evt_OnPieceUnselected(MahjongPiece pc)
    {
        OnPieceUnselected.Invoke(pc);
    }

    public void Evt_OnPieceHoverEnter()
    {
        OnPieceHoverEnter.Invoke();
    }
    public void Evt_OnPieceHoverExit()
    {
        OnPieceHoverExit.Invoke();
    }
}

