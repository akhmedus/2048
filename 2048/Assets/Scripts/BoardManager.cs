using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public TileManager prefab;
    GridManager grid;
    List<TileManager> tiles;
    public TileDesign[] designs;

    void Awake()
    {
        grid=GetComponentInChildren<GridManager>();
        tiles=new List<TileManager>(16);
    }

    void CreateTile()
    {
        var tile = Instantiate(prefab,grid.transform);
        tile.SetDesign(designs[0],2);
        tile.SpawnTile(grid.GetEmptyCell());
        tiles.Add(tile);
    }

    void Start()
    {
        CreateTile();
        CreateTile();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            DragAll(Vector2Int.up, 0, 1, 1, 1);
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            DragAll(Vector2Int.left, 1, 1, 0, 1);
        } 
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            DragAll(Vector2Int.down, 0, 1, grid.GridHeight() - 2, -1);
        } 
        else if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            DragAll(Vector2Int.right, grid.GridWidth() - 2, -1, 0, 1);
        }
    }

    private void DragAll(Vector2Int vector, int startX, int stepX, int startY, int stepY)
    {
        for (int x = startX; x >= 0 && x < grid.GridWidth(); x += stepX)
        {
            for (int y = startY; y >= 0 && y < grid.GridHeight(); y += stepY)
            {
                var cell = grid.GetCell(x, y);

                if (cell.IsNotEmpty()) {
                    Drag(cell.tile, vector);
                }
            }
        }
    }

    private void Drag(TileManager tileManager, Vector2Int vector)
    {
        CellManager newCell=null;
        CellManager neighborCell=grid.GetNeighborCell(tileManager.cell,vector);


        while (neighborCell!=null)
        {
            if(neighborCell.IsNotEmpty())
            {
                break;
            }

            newCell=neighborCell;
            neighborCell=grid.GetNeighborCell(neighborCell,vector);
        }

        if(newCell!=null)
        {
            tileManager.Moving(newCell);
        }
    }
}
