using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int position;
    [HideInInspector]
    public TileManager tile;

    public bool IsEmpty()
    {
        return tile==null;
    }

    public bool IsNotEmpty()
    {
        return tile!=null;
    }
}
