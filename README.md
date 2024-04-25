# Mahjong Solitaire

This small documentation will serve as an introduction to some of the systems I designed in this project. It does not aim to explain every single script in the game.

## Grid Manager
The purpose of the Grid Manager is solely to create a 3D array of `GridTiles` and assign them coordinates indicating the position in the grid when calling `SetUpTile()`. It stores all tiles into a 1D list and also in a 3D Array. The first is used on simple operations of iteration such as resetting all tiles and the latter is used to retrieve tiles above, below or next to the given tile. At the current stage of development, the X and Y of the grid are pre-defined while the Z axis is defined by the amount of layers the game will have with MohjangTiles to match (A list of `LayoutData` in the GridManager).


### Retrieving Tiles

```C#
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
```


## Game Manager

This script controls the flow of the game and implements a Singleton pattern. It should always be present in the scene and this manager also creates the `DeckManager` and the `GridManager`.Its methods (eg. `MatchAttempt()`) controls and manages the rules of the game and are subscribed as listeners to some events from other MonoBehaviours while also having events of its own.

#### StartGame() & ResetGame()
They can be called at any moment to delete and clear the whole board or start spawning tiles in it.
#### Feedbacks
There is a `#REGION` that handles spawning effects for each action such as a invalid selection or a successful match.
#### Lists, Arrays and Iterations
- There are no complex algorithms in the code but the code of checking if there are still matches to be made happens inside a Coroutine to keep it asyncronous from the main code. This way it avoids stutters or delays everytime you match two tiles.
- The list `_selectedPieces` keeps track of how many tiles are selected by the player. This allows for extensions in the code such as a necessity of match 4 tiles at the same time. 
- The `_allPieces` list keeps track of the amount of remaining tiles of each symbol. This helps the coroutine to check remaining matches with a little bit more optimization.

## Layout Data
This is a scriptable object capable of serializing a 2D Array of booleans and with this array, it allows a easy and simple "Level Building" tool. You can simply select the slots on the matrix shown in the inspector, drag the Scriptable Object to the respective field in the GridManager and the GridManager will use this as a filter for available grid slots.

#### LayoutSize and Matrix
2D arrays are not serializable by default so it's necessary to store all this information into a 1D Array/List and display them using a custom editor script (`LayoutDataEditor`). The LayoutSize should contain the same X and Y values that the GridManager is using to create the grid. A simple implementation of code could fix this so the grid always use the value of the biggest Layout received.
#### Methods
It has simple methods to get or set values at specific coordinates and also a method to convert the current 1D array of booleans into a 2D Array. The `ResizeMatrix` method is used in the inspector to update the serialization.

### DeckManager Class
The `DeckManager` class manages the spawning of Mahjong tiles and positioning of them. It also takes care of offsets in the generation of different layers so sprites don't overlap themselves. Currently it requires a list of the types of tiles in the game and with this list it will populate the grid with the correct amount for every tile to have a match.

#### Fields:
- `layersOffset`: Offset between layers of tiles.
- `amountPerCard`: Number of instances per tile, needs to be changed for now.

#### Methods:
- `SpawnAndSpreadTiles(int amount)`: Spawns and spreads tiles on the grid with a interpolation/animation effect of tiles moving to their location.

### MahjongPiece

#### Overview:
The `MahjongPiece` class represents a single Mahjong tile in the game. It controls all the interactions and behaviour of only one single piece.
