using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    private GameState gameState = GameState.Intro;
    public static int m_ScoreValue = 0;
    public static int m_PointsValue = 0;
    public static bool GameIsIntro = true;
    public static bool GameIsPaused = false;
    private bool PauseButtonisActive = false;
    private float Timer = 10;

    [SerializeField] private int m_DifficultyScore = 50;
    [SerializeField] private int m_PauseCost = 10;
    [SerializeField] private TextMeshProUGUI m_Score = null;
    [SerializeField] private TextMeshProUGUI m_Points = null;
    [SerializeField] private GameObject m_GameOverCanvas;
    [SerializeField] private GameObject m_GameStartCanvas;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private TextMeshProUGUI TimerText2;
    [SerializeField] private TextMeshProUGUI TimerText3;
    [SerializeField] private TextMeshProUGUI TimerText4;

    public GameState CurGameState
    {
        get { return gameState; }
    }

    private void Awake()
    {
        m_ScoreValue = 0;
        m_PointsValue = 100;
        Target.m_TargetSpeed = 1f;
        Blaster.m_MaxProjectileCount = 25;
        Blaster.m_FiredCount = 0;
        EnemySpawn.m_SpawnInterval = 3f;
    }

    private void Update()
    {
        
        if(gameState == GameState.Intro)
        {
            Time.timeScale = 0f;
            Blaster.m_IsRealoading = false;
            GameIsIntro = true;
        }

        if (gameState == GameState.Play)
        {
            Time.timeScale = 1f;
            GameIsIntro = false;
            m_GameStartCanvas.SetActive(false);
        }

        if (gameState == GameState.Pause)
        {
            
            PauseButtonisActive = true;
            GameIsPaused = true;
            Timer -=  Time.unscaledDeltaTime;
            
            TimerText.text = ("" + Timer );
            TimerText2.text = ("" + Timer);
            TimerText3.text = ("" + Timer);
            TimerText4.text = ("" + Timer);

            if (Timer < 0)
            {
                Timer += 10;
                gameState = GameState.Play;
                GameIsPaused = false;
                PauseButtonisActive = false;
                TimerText.text = ("Pause Game for 10 Seconds    (10 Points)");
                TimerText2.text = ("Pause Game for 10 Seconds    (10 Points)");
                TimerText3.text = ("Pause Game for 10 Seconds    (10 Points)");
                TimerText4.text = ("Pause Game for 10 Seconds    (10 Points)");
            }
        }

        if(gameState == GameState.GameOver)
        {
            Time.timeScale = 0f;
            m_GameOverCanvas.SetActive(true);
            GameIsIntro = true;
        }

        if (m_ScoreValue > m_DifficultyScore)
        {
            Target.m_TargetSpeed += 1f;
            EnemySpawn.m_SpawnInterval -= 0.1f;
            m_DifficultyScore += 100;
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
        if (Game.GameIsIntro == true)
            return;
        if (PauseButtonisActive == true)
            return;
        if (m_PointsValue < m_PauseCost)
            return;
        m_PointsValue -= m_PauseCost;
        
        gameState = GameState.Pause;
    }

    public enum GameState
    {
        Intro, Play , Pause,  GameOver
    }

   
}
