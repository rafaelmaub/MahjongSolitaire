using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private GameManager Manager => GameManager.Instance;

    [SerializeField] private int amountPerCard = 4;
    [SerializeField] private MahjongPiece piecePrefab;
    [SerializeField] private List<TileData> tilesDatabase = new List<TileData>(); //TODO: Store all tiles into another scriptable object and assign the object here instead

    public void InitializeTileDatas()
    {
        foreach(TileData t in tilesDatabase)
        {
            Manager.AddTileTypeToAllPieces(t);
        }
    }

    TileData GetRandomTileData()
    {
        return tilesDatabase[Random.Range(0, tilesDatabase.Count)];
    }

    public void SpawnAndSpreadTiles(int amount)
    {
        if(amount % 2 != 0)
        {
            amount--;
        }

        //int amountPerCard = amount / tilesDatabase.Count;

        foreach(TileData data in tilesDatabase)
        {
            for (int i = 0; i < amountPerCard; i++)
            {
                MahjongPiece pc = SpawnPiece(data);
                GridTile tempTile = Manager.Grid.GetRandomTile(true);

                tempTile.LinkPiece(pc);
                pc.LinkToTileSlot(tempTile);

                pc.transform.position = tempTile.transform.position;

                pc.Interactions.OnPieceSelected += Manager.SelectedPiece;
                pc.Interactions.OnPieceUnselected += Manager.UnselectedPiece;
                pc.Interactions.OnInvalidClick += Manager.InvalidClick;

                Manager.PieceSpawned(pc);
            }
        }

    }

    MahjongPiece SpawnPiece(TileData tileData)
    {
        MahjongPiece tempPiece = Instantiate(piecePrefab, transform);
        tempPiece.SetupPiece(tileData);

        return tempPiece;
    }

    public void ResetDeck()
    {

    }
}
