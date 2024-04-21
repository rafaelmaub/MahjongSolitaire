using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mahjong Piece")]
public class TileData : ScriptableObject
{
    public Sprite SpriteVisual => spriteVisual;
    [SerializeField] private Sprite spriteVisual;
}
