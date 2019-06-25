using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    private GameState gameState = GameState.Intro;
    public static int m_ScoreValue = 0;
    public static int m_PointsValue = 100;
    private int m_DifficultyScore = 50;
    [SerializeField] private int m_PauseCost = 10;
    public TextMeshProUGUI m_Score = null;
    public TextMeshProUGUI m_Points = null;
    public AudioSource GameMusic;

    public GameObject m_GameOverCanvas;


    private float Timer = 10;

    public GameState CurGameState
    {
        get { return gameState; }
    }

    private void Update()
    {
        
        if(gameState == GameState.Intro)
        {
            m_ScoreValue = 0;
            m_PointsValue = 0;
            Time.timeScale = 0f;
        }

        if (gameState == GameState.Play)
        {
            Time.timeScale = 1f;
            //GameMusic.Play();
        }

        if (gameState == GameState.Pause)
        {
            
            Timer -=  Time.unscaledDeltaTime;
            Time.timeScale = 0f;
            if (Timer < 0)
            {
                Timer += 10;
                gameState = GameState.Play;
            }
        }

        if(gameState == GameState.GameOver)
        {
            Time.timeScale = 0f;
            m_GameOverCanvas.SetActive(true);
        }

        if (m_ScoreValue > m_DifficultyScore)
        {
            Target.m_TargetSpeed += 1f;
            m_DifficultyScore += 50;
        }
        

        m_Score.text = "" + m_ScoreValue;
        m_Points.text = "" + m_PointsValue;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("EnemyTarget"))
        {
            gameState = GameState.GameOver;
        }
            
            
    }
    public void StartGame()
    {
        gameState = GameState.Play;
    }

    public void Pause()
    {
        if (m_PointsValue < m_PauseCost) { return; }
        m_PointsValue -= m_PauseCost;
        gameState = GameState.Pause;

    }


    
    public enum GameState
    {
        Intro, Play , Pause,  GameOver
    }

   
}
