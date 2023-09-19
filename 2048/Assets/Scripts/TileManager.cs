using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    [HideInInspector]
    public TileDesign design;
    [HideInInspector]
    public CellManager cell;
    [HideInInspector]
    public int numeric;

    Image bg;
    TextMeshProUGUI text;

    void Awake()
    {
        bg=GetComponent<Image>();
        text=GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetDesign(TileDesign tileDesign, int value)
    {
        design=tileDesign;
        numeric=value;

        bg.color=design.bgColor;
        text.color=design.txtColor;
        text.text=numeric.ToString();
    }

    public void SpawnTile(CellManager cellManager)
    {
        if(cell!=null)
        {
            cell.tile=null;
        }

        cell=cellManager;
        cell.tile=this;

        transform.position=cell.transform.position;
    }

    public void Moving(CellManager cellManager)
    {
        if (cell != null) {
            cell.tile = null;
        }

        cell = cellManager;
        cell.tile = this;

        transform.position=cell.transform.position;
    }
}
