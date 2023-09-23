using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public BoardManager boardManager;
    public CanvasGroup canvasGameOver;

    public TextMeshProUGUI txtPoints;
    public TextMeshProUGUI txtBestPoints;
    int points;
    void Start()
    {

        StartGame();
    }

    public void StartGame()
    {
        SetPoints(0);
        txtBestPoints.text = BestPoints().ToString();
        
        canvasGameOver.alpha = 0;
        canvasGameOver.interactable = false;

        boardManager.DestroyTile();
        boardManager.CreateTile();
        boardManager.CreateTile();
        boardManager.enabled = true;
    }

    public void GameOver()
    {
        boardManager.enabled = false;
        canvasGameOver.interactable = true;
        StartCoroutine(CanvasSetting(canvasGameOver, 1f, 1f));
    }
    IEnumerator CanvasSetting(CanvasGroup canvas, float value, float delay)
    {
        yield return new WaitForSeconds(delay);

        float expired = 0;
        float lenght = .5f;
        var alph = canvas.alpha;

        while (expired < lenght)
        {
            canvas.alpha = Mathf.Lerp(alph, value, expired / lenght);
            expired += Time.deltaTime;
            yield return null;
        }

        canvas.alpha = value;
    }

    void SetPoints(int value)
    {
        points = value;
        txtPoints.text = value.ToString();

        SetBestPoints();
    }

    void SetBestPoints()
    {
        int bestPoints = BestPoints();

        if (points > bestPoints)
        {
            PlayerPrefs.SetInt("bestPoints", points);
        }
    }

    int BestPoints()
    {
        return PlayerPrefs.GetInt("bestPoints", 0);
    }

    public void GetPoints(int value)
    {
        SetPoints(points + value);
    }
}
