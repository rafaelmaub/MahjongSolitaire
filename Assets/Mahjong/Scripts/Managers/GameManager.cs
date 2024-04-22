using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Initialization
    public static GameManager Instance => instance;
    private static GameManager instance;
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        grid = Instantiate(grid);
        deck = Instantiate(deck);
    }

#endregion

    public GridManager Grid => grid;
    public DeckManager Deck => deck;

    [SerializeField] private GridManager grid;
    [SerializeField] private DeckManager deck;
    [SerializeField] private int requiredAmountToMatch = 2;

    [Header("Feedbacks")]
    [SerializeField] private GameObject invalidClickFeedback;

    private List<MahjongPiece> _selectedPieces = new List<MahjongPiece>();
    [SerializeField] private List<AmountOfTile> _allPieces = new List<AmountOfTile>();

    private void Start()
    {
        Grid.CreateGrid();
        StartGame();
    }

    void StartGame()
    {
        Deck.InitializeTileDatas();
        Deck.SpawnAndSpreadTiles(grid.Rows * grid.Columns);

    }



    //TODO: IMPROVE ALGORITHM TO FIND INDEX ON BIGGER LISTS
    public void PieceSpawned(MahjongPiece piece)
    {
        GetAllTilesFromData(piece.TileData).AddPieceToList(piece);
    }
    public void SelectedPiece(MahjongPiece piece)
    {
        _selectedPieces.Add(piece);
        if(_selectedPieces.Count >= requiredAmountToMatch)
        {
            MatchAttempt();
        }
    }

    public void UnselectedPiece(MahjongPiece piece)
    {
        _selectedPieces.Remove(piece);

    }

    void MatchAttempt()
    {
        TileData data = _selectedPieces[0].TileData;
        bool success = true;

        foreach(MahjongPiece p in _selectedPieces)
        {
            if(p.TileData != data)
            {
                success = false;
                break;
            }
        }

        if (success) SuccessMatch();
        else FailedMatch();

    }

    void FailedMatch()
    {
        MahjongPiece[] pieces = _selectedPieces.ToArray();
        foreach (MahjongPiece p in pieces)
        {
            p.UnselectTile();
        }

        
    }

    void SuccessMatch()
    {
        AmountOfTile tileList = GetAllTilesFromData(_selectedPieces[0].TileData);
        foreach (MahjongPiece p in _selectedPieces)
        {
            tileList.RemovePieceFromList(p);
            Destroy(p.gameObject);
        }

        _selectedPieces.Clear();
        StartCoroutine(CheckForRemainingPieces());
    }
    
    IEnumerator CheckForRemainingPieces()
    {
        int amountOfCards = 0;
        foreach(AmountOfTile ap in _allPieces)
        {
            amountOfCards += ap.Count;
        }

        if(amountOfCards > 0)
        {
            bool isMatchAvailable = false;
            foreach (AmountOfTile ap in _allPieces)
            {
                int availableCount = ap.AvailableCount;
                if(availableCount >= 2)
                {
                    isMatchAvailable = true;
                    break;
                }
                
            }

            if (!isMatchAvailable)
            {
                GameLose();
            }

        }
        else
        {
            GameWin();
        }
        
        yield return null;
    }
    void GameWin()
    {
        Debug.Log("WIN");
    }

    void GameLose()
    {
        Debug.LogError("NO MORE MATCHES");
    }

    public void AddTileTypeToAllPieces(TileData data)
    {
        _allPieces.Add(new AmountOfTile(data));
    }
    AmountOfTile GetAllTilesFromData(TileData data)
    {
        return _allPieces.Where(p => p.IsThisList(data)).FirstOrDefault();
    }

    #region Feedbacks

    public void InvalidClick(MahjongPiece pc)
    {
        Instantiate(invalidClickFeedback, (Vector2)pc.transform.position + (Random.insideUnitCircle / 8.5f), Quaternion.identity);
    }
    #endregion
}


[System.Serializable]
public class AmountOfTile
{
    public int Count => piecesOfThisTile.Count;
    public int AvailableCount => piecesOfThisTile.Where(p => p.IsPlayable()).Count();
    public string Name => myData.name;

    [SerializeField] private TileData myData;
    [SerializeField] private List<MahjongPiece> piecesOfThisTile;
    public AmountOfTile(TileData data)
    {
        myData = data;
        piecesOfThisTile = new List<MahjongPiece>();
    }

    public bool IsThisList(TileData data)
    {
        return myData == data;
    }

    public void AddPieceToList(MahjongPiece pc)
    {
        piecesOfThisTile.Add(pc);
    }

    public void RemovePieceFromList(MahjongPiece pc)
    {
        piecesOfThisTile.Remove(pc);
    }

}