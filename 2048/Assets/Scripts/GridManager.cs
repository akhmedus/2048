using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [HideInInspector]
    public RowManager[] rows;
    [HideInInspector]
    public CellManager[] cells;

    public int GridSize()
    {
        return cells.Length;
    }
    public int GridHeight()
    {
        return rows.Length;
    }
    public int GridWidth()
    {
        return GridSize()/GridHeight();
    }

    void Awake()
    {
        rows=GetComponentsInChildren<RowManager>();
        cells=GetComponentsInChildren<CellManager>();
    }

    void Start()
    {
        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].cells.Length; x++)
            {
                rows[y].cells[x].position=new Vector2Int(x,y);
            }
        }
    }

    public CellManager GetCell(Vector2Int positions)
    {
        return GetCell(positions.x, positions.y);
    }

    public CellManager GetCell(int x, int y)
    {
        if (x >= 0 && x < GridWidth() && y >= 0 && y < GridHeight()) {
            return rows[y].cells[x];
        } else {
            return null;
        }
    }

    public CellManager GetNeighborCell(CellManager cellManager, Vector2Int vector)
    {
        var cellPos=cellManager.position;

        cellPos.x+=vector.x;
        cellPos.y+=vector.y;
        
        return GetCell(cellPos);
    }

    public CellManager GetEmptyCell()
    {
        int index=Random.Range(0,cells.Length);
        int startIndex=index;

        while (cells[index].IsNotEmpty())
        {
            index++;

            if(index>=cells.Length)
            {
                index=0;
            }

            if(index==startIndex)
            {
                return null;
            }
        }

        return cells[index];
    }
}
