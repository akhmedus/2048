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
    [HideInInspector]
    public bool locked;

    Image bg;
    TextMeshProUGUI text;

    void Awake()
    {
        bg = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetDesign(TileDesign tileDesign, int value)
    {
        design = tileDesign;
        numeric = value;

        bg.color = design.bgColor;
        text.color = design.txtColor;
        text.text = numeric.ToString();
    }

    public void SpawnTile(CellManager cellManager)
    {
        if (cell != null)
        {
            cell.tile = null;
        }

        cell = cellManager;
        cell.tile = this;

        transform.position = cellManager.transform.position;
    }

    public void Moving(CellManager cellManager)
    {
        if (cell != null)
        {
            cell.tile = null;
        }

        cell = cellManager;
        cell.tile = this;

        StartCoroutine(MoveAnimation(cellManager.transform.position, false));
    }

    public void TileMerge(CellManager cellManager)
    {
        if (cell != null)
        {
            cell.tile = null;
        }

        cell = null;
        cellManager.tile.locked = true;

        StartCoroutine(MoveAnimation(cellManager.transform.position, true));

    }

    IEnumerator MoveAnimation(Vector3 vector, bool merging)
    {
        float timePassed = 0;
        float timeLenght = .1f;

        var startPos = transform.position;

        while (timePassed < timeLenght)
        {
            transform.position = Vector3.Lerp(startPos, vector, timePassed / timeLenght);
            timePassed += Time.deltaTime;
            yield return null;
        }

        transform.position = vector;

        if (merging)
        {
            Destroy(gameObject);
        }
    }
}
