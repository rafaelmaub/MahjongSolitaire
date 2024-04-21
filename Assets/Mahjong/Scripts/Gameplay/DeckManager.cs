using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private MahjongPiece piecePrefab;
    [SerializeField] private List<TileData> tilesDatabase = new List<TileData>(); //TODO: Store all tiles into another scriptable object and assign the object here instead


    public void SpawnAndSpreadTiles(int amount)
    {

        int amountPerCard = tilesDatabase.Count / amount; //TODO: avoid "odd" numbers

        for(int i = 0; i < amount; i++)
        {

        }
    }
    MahjongPiece SpawnPiece(TileData tileData)
    {
        MahjongPiece tempPiece = Instantiate(piecePrefab, transform);
        tempPiece.SetupPiece(tileData);

        return tempPiece;
    }
}
