using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    private GameManager Manager => GameManager.Instance;
    public int Rows => gridSize.y;
    public int Columns => gridSize.x;
    public int Depth => _layouts.Count;
    public int TilesInGame => _availableTiles.Count;

    [SerializeField] private Vector2 startingPosition;
    [SerializeField] private Vector3Int gridSize;
    [SerializeField] private GridTile tilePrefab;
    [SerializeField] private List<LayoutData> _layouts = new List<LayoutData>();

    private GridTile[,,] _grid;
    private List<GridTile> _tiles = new List<GridTile>();
    private List<GridTile> _availableTiles = new List<GridTile>();
    
    private void Awake()
    {
        transform.position = Vector2.zero;
    }
    public void ResetGrid()
    {
        foreach (GridTile t in _tiles)
        {
            t.ResetTile();
        }

        ValidateTiles();
    }

    public void CreateGrid()
    {
        _grid = new GridTile[gridSize.x, gridSize.y, _layouts.Count];

        

        //float spacingZ = tilePrefab.transform.localScale.y;

        for(int z = 0; z < _layouts.Count; z++)
        {
            Vector2 currentPos = startingPosition;
            float spacingX = tilePrefab.transform.localScale.x;
            float spacingY = tilePrefab.transform.localScale.y;

            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {

                    GridTile tempTile = Instantiate(tilePrefab, currentPos, Quaternion.identity);
                    
                    _grid[x, y, z] = tempTile;
                    currentPos.x += spacingX;

                    tempTile.transform.SetParent(transform);
                    tempTile.SetUpTile(new Vector3Int(x, y, z));

                    _tiles.Add(tempTile);

                }

                currentPos.x = startingPosition.x;
                currentPos.y -= spacingY;
            }
        }

        ValidateTiles();


    }

    public GridTile GetRandomTile(bool empty = false)
    {
        if(!empty)
        {
            return _tiles[UnityEngine.Random.Range(0, _tiles.Count)];
        }
        else
        {
            GridTile tempTile = _availableTiles[UnityEngine.Random.Range(0, _availableTiles.Count)];
            _availableTiles.Remove(tempTile);
            return tempTile;
            
        }
    }
    public GridTile GetAboveTile(Vector3Int coordinates)
    {
        if (coordinates.z >= _layouts.Count - 1 || _layouts.Count == 1)
        {
            return null;
        }
        else
        {
            return _grid[coordinates.x, coordinates.y, coordinates.z + 1];
        }
    }
    public GridTile GetPreviousTile(Vector3Int coordinates)
    {
        if(coordinates.x <= 0)
        {
            return null;
        }
        else
        {
            return _grid[coordinates.x - 1, coordinates.y, coordinates.z];
        }
    }
    public GridTile GetNextTile(Vector3Int coordinates)
    {
        if (coordinates.x >= gridSize.x - 1)
        {
            return null;
        }
        else
        {
            return _grid[coordinates.x + 1, coordinates.y, coordinates.z];
        }
    }

    void ValidateTiles()
    {
        _availableTiles = new List<GridTile>();

        if(_layouts.Count == 0)
        {
            Debug.LogWarning("There are no grid slots in the game!");
            _availableTiles = _tiles.ToList();
        }
        else
        {
            for (int z = 0; z < _layouts.Count; z++)
            {
                bool[,] matrix = LayoutData.ConvertTo2DArray(_layouts[z].Matrix, _layouts[z].LayoutSize.x, _layouts[z].LayoutSize.y);
                //bool[,] matrix = matrixTuple.Item1;
                //Vector2Int origin = matrixTuple.Item2;


                for (int y = 0; y < gridSize.y; y++)
                {
                    for (int x = 0; x < gridSize.x; x++)
                    {
                        if (matrix[x, y])
                        {
                            _availableTiles.Add(_grid[x, y, z]);
                        }

                    }


                }
            }
        }


    }

    #region Buttons
    public void AddLayer(LayoutData layer)
    {
        _layouts.Insert(0, layer);
    }

    public void RemoveLayer()
    {
        _layouts.RemoveAt(0);
    }

    #endregion
}
