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

    private void Start()
    {
        Grid.CreateGrid();
        StartGame();
    }
    void StartGame()
    {
        Deck.SpawnAndSpreadTiles(grid.Rows * grid.Columns);
    }
}
