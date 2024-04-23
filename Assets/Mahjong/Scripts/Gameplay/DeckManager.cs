using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private GameManager Manager => GameManager.Instance;


    [SerializeField] private int amountPerCard = 4;
    [SerializeField] private MahjongPiece piecePrefab;
    [SerializeField] private List<TileData> tilesDatabase = new List<TileData>(); //TODO: Store all tiles into another scriptable object and assign the object here instead

    private List<MahjongPiece> _allPiecesInstances;
    [Header("Visuals")]
    [SerializeField] private float tilesShuffleSpeed;
    [SerializeField] private AudioClip shuffleSound;
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
    IEnumerator SpreadTiles()
    {
        yield return new WaitForSeconds(0.2f);
        List<PieceAndTileLink> coroutineList = new List<PieceAndTileLink>();
        _allPiecesInstances = new List<MahjongPiece>();

        foreach (TileData data in tilesDatabase)
        {
            for (int i = 0; i < amountPerCard; i++)
            {
                MahjongPiece pc = SpawnPiece(data);
                GridTile tempTile = Manager.Grid.GetRandomTile(true);

                tempTile.LinkPiece(pc);
                pc.LinkToTileSlot(tempTile);

                _allPiecesInstances.Add(pc);
                coroutineList.Add(new PieceAndTileLink(pc, tempTile));


                pc.Interactions.OnPieceSelected += Manager.SelectedPiece;
                pc.Interactions.OnPieceUnselected += Manager.UnselectedPiece;
                pc.Interactions.OnInvalidClick += Manager.InvalidClick;

                Manager.PieceSpawned(pc);
            }
        }

        StartCoroutine(MovePiecesOnGrid(coroutineList));
    }
    public void SpawnAndSpreadTiles(int amount)
    {
        if(amount % 2 != 0)
        {
            amount--;
        }

        StartCoroutine(SpreadTiles());
        //int amountPerCard = amount / tilesDatabase.Count;

    }

    IEnumerator MovePiecesOnGrid(List<PieceAndTileLink> list)
    {
        foreach(PieceAndTileLink pt in list)
        {
            GridTile tile = pt.tile;
            MahjongPiece pc = pt.piece;

            if(SoundManager.Instance && shuffleSound)
            {
                SoundManager.Instance.PlayCustomOneShot(shuffleSound);
            }

            while (Vector2.Distance(tile.transform.position, pc.transform.position) > 0.1f)
            {
                pc.transform.position = Vector2.Lerp(pc.transform.position, tile.transform.position, Time.deltaTime * tilesShuffleSpeed);
                yield return null;
            }

            pc.transform.position = tile.transform.position;
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
        foreach(MahjongPiece p in _allPiecesInstances)
        {
            if (p) Destroy(p.gameObject);
        }
    }
}

public class PieceAndTileLink
{
    public MahjongPiece piece;
    public GridTile tile;

    public PieceAndTileLink(MahjongPiece p, GridTile g)
    {
        piece = p;
        tile = g;
    }
}

