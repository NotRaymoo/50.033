using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;
    public GameObject gameOverCanvas;
    private int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        gameOverCanvas.GetComponent<Canvas>().planeDistance = 0;
    }

    public void GameRestart()
    {
        gameOverCanvas.GetComponent<Canvas>().planeDistance = 0;
        scoreText.text = "Score: 0";
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        gameOverCanvas.GetComponent<Canvas>().planeDistance = 1;
        gameOverScoreText.text = scoreText.text;
    }
}
