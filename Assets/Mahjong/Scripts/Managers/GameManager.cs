using System.Collections;
using System.Collections.Generic;
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
    private List<MahjongPiece> selectedPieces = new List<MahjongPiece>();
    private void Start()
    {
        Grid.CreateGrid();
        StartGame();
    }
    void StartGame()
    {
        Deck.SpawnAndSpreadTiles(grid.Rows * grid.Columns);

    }

    public void SelectedPiece(MahjongPiece piece)
    {
        selectedPieces.Add(piece);
        if(selectedPieces.Count >= 2)
        {
            MatchAttempt();
        }
    }

    public void UnselectedPiece(MahjongPiece piece)
    {
        selectedPieces.Remove(piece);

    }

    void MatchAttempt()
    {
        TileData data = selectedPieces[0].TileData;
        bool success = true;

        foreach(MahjongPiece p in selectedPieces)
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

        MahjongPiece[] pieces = selectedPieces.ToArray();
        foreach (MahjongPiece p in pieces)
        {
            p.UnselectTile();
            //p.Interactions.OnPieceUnselected -= UnselectedPiece;
            //p.Interactions.OnPieceSelected -= UnselectedPiece;
        }

        
    }

    void SuccessMatch()
    {
        foreach (MahjongPiece p in selectedPieces)
        {
            Destroy(p.gameObject);
        }

        selectedPieces.Clear();
    }
}
