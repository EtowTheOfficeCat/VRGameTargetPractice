using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    private GameState gameState = GameState.Intro;
    public static int m_ScoreValue = 0;
    public static int m_PointsValue = 2;
    public TextMeshProUGUI m_Score = null;
    public TextMeshProUGUI m_Points = null;
    public AudioSource GameMusic;

    private void Update()
    {
        if(gameState == GameState.Intro)
        {
            Time.timeScale = 0f;
        }

        if (gameState == GameState.Play)
        {
            Time.timeScale = 1f;
            //GameMusic.Play();
        }

        else if (gameState == GameState.Pause)
        {
            Time.timeScale = 0f;
        }

        m_Score.text = "" + m_ScoreValue;
        m_Points.text = "" + m_PointsValue;
    }
    public void StartGame()
    {
        gameState = GameState.Play;
    }

    public void Pause()
    {
        gameState = GameState.Pause;
    }

    enum GameState
    {
        Intro, Play , Pause,  GameOver
    }
}
