using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GameManager Manager => GameManager.Instance;
    public int Rows => gridSize.y;
    public int Columns => gridSize.x;
    public int Depth => gridSize.z;

    [SerializeField] private Vector2 startingPosition;
    [SerializeField] private Vector3Int gridSize;
    [SerializeField] private GridTile tilePrefab;

    private GridTile[,,] _grid;
    private List<GridTile> _tiles = new List<GridTile>();
    private List<GridTile> _availaleTiles;

    
    private void Awake()
    {
        transform.position = Vector2.zero;
    }

    public void CreateGrid()
    {
        _grid = new GridTile[gridSize.x, gridSize.y, gridSize.z];

        Vector2 currentPos = startingPosition;

        float spacingX = tilePrefab.transform.localScale.x;
        float spacingY = tilePrefab.transform.localScale.y;
        //float spacingZ = tilePrefab.transform.localScale.y;

        for(int z = 0; z < gridSize.z; z++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {

                    GridTile tempTile = Instantiate(tilePrefab, currentPos, Quaternion.identity);
                    _grid[x, y, z] = tempTile;
                    currentPos.x += spacingX;
                    tempTile.transform.SetParent(transform);
                    _tiles.Add(tempTile);
                }

                currentPos.x = startingPosition.x;
                currentPos.y -= spacingY;
            }
        }
        _availaleTiles = _tiles.ToList();

    }

    public GridTile GetRandomTile(bool empty = false)
    {
        if(!empty)
        {
            return _tiles[Random.Range(0, _tiles.Count)];
        }
        else
        {
            GridTile tempTile = _availaleTiles[Random.Range(0, _availaleTiles.Count)];
            _availaleTiles.Remove(tempTile);
            return tempTile;
            
        }
    }
}
