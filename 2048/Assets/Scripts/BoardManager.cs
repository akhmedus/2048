using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public TileManager prefab;
    GridManager grid;
    List<TileManager> tiles;
    public TileDesign[] designs;

    bool stopTime;
    void Awake()
    {
        grid = GetComponentInChildren<GridManager>();
        tiles = new List<TileManager>(16);
    }

    void CreateTile()
    {
        var tile = Instantiate(prefab, grid.transform);
        tile.SetDesign(designs[0], 2);
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
        if (!stopTime)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                DragAll(Vector2Int.up, 0, 1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                DragAll(Vector2Int.down, 0, 1, grid.GridHeight() - 2, -1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                DragAll(Vector2Int.left, 1, 1, 0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                DragAll(Vector2Int.right, grid.GridWidth() - 2, -1, 0, 1);
            }
        }
    }

    private void DragAll(Vector2Int vector, int startX, int stepX, int startY, int stepY)
    {
        bool stopTimeState = false;
        for (int x = startX; x >= 0 && x < grid.GridWidth(); x += stepX)
        {
            for (int y = startY; y >= 0 && y < grid.GridHeight(); y += stepY)
            {
                var cell = grid.GetCell(x, y);

                if (cell.IsNotEmpty())
                {
                    stopTimeState |= Drag(cell.tile, vector);
                }
            }
        }

        if (stopTimeState)
        {
            StartCoroutine(WaitForStop());
        }
    }

    private bool Drag(TileManager tileManager, Vector2Int vector)
    {
        CellManager newCell = null;
        CellManager neighborCell = grid.GetNeighborCell(tileManager.cell, vector);


        while (neighborCell != null)
        {
            if (neighborCell.IsNotEmpty())
            {
                if (TilesJoining(tileManager, neighborCell.tile))
                {
                    TilesMerge(tileManager, neighborCell.tile);
                    return true;
                }
                break;
            }

            newCell = neighborCell;
            neighborCell = grid.GetNeighborCell(neighborCell, vector);
        }

        if (newCell != null)
        {
            tileManager.Moving(newCell);
            return true;
        }

        return false;
    }
    IEnumerator WaitForStop()
    {
        stopTime = true;

        yield return new WaitForSeconds(.1f);

        stopTime = false;

        foreach (var tile in tiles)
        {
            tile.locked = false;
        }

        if (tiles.Count != grid.GridSize())
        {
            CreateTile();
        }
    }

    bool TilesJoining(TileManager tileManager1, TileManager tileManager2)
    {
        return tileManager1.design == tileManager2.design && !tileManager2.locked;
    }

    void TilesMerge(TileManager tileManager1, TileManager tileManager2)
    {
        tiles.Remove(tileManager1);
        tileManager1.TileMerge(tileManager2.cell);
        int tileIndex = Mathf.Clamp(GetTileIndex(tileManager2.design) + 1, 0, designs.Length - 1);
        int numeric = tileManager2.numeric * 2;
        tileManager2.SetDesign(designs[tileIndex], numeric);
    }

    int GetTileIndex(TileDesign design)
    {
        for (int i = 0; i < designs.Length; i++)
        {
            if (design == designs[i])
            {
                return i;
            }
        }
        return -1;
    }
}
