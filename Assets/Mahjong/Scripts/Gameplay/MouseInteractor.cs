using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteractor : PieceAttachment, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Piece.SelectTile();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Piece.Interactions.Evt_OnPieceHoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Piece.Interactions.Evt_OnPieceHoverExit();
    }


}
