using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private GameState gameState = GameState.Intro;

    private void Update()
    {
        if(gameState == GameState.Intro)
        {
            Time.timeScale = 0f;
        }

        if (gameState == GameState.Play)
        {
            Time.timeScale = 1f;
        }

        else if (gameState == GameState.Pause)
        {
            Time.timeScale = 0f;
        }
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
