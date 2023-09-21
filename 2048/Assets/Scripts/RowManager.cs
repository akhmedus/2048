using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowManager : MonoBehaviour
{
    [HideInInspector]
    public CellManager[] cells;

    void Awake()
    {
        cells = GetComponentsInChildren<CellManager>();
    }
}
