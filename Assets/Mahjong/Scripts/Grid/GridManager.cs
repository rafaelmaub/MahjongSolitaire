using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    
    [SerializeField] private Vector2 startingPosition;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private GridTile tilePrefab;

    GridTile[,] _grid;

    private void Awake()
    {
        transform.position = Vector2.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGrid()
    {
        _grid = new GridTile[gridSize.x, gridSize.y];

        Vector2 currentPos = startingPosition;

        float spacingX = tilePrefab.transform.localScale.x;
        float spacingY = tilePrefab.transform.localScale.y;

        for(int y = 0; y < gridSize.y; y++)
        {
            for(int x = 0; x < gridSize.x; x++)
            {

                GridTile tempTile = Instantiate(tilePrefab, currentPos, Quaternion.identity);
                _grid[x, y] = tempTile;
                currentPos.x += spacingX;
                tempTile.transform.SetParent(transform);
            }

            currentPos.x = startingPosition.x;
            currentPos.y -= spacingY;
        }
    }


}
