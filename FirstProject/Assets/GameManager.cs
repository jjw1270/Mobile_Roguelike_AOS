using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    ingame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentGameState = GameState.menu;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (Input.GetButtonDown("S"))
        {
            StartGame();
        }
    }

    void SetGameState (GameState newGameState)
    {
        if (newGameState == GameState.menu) { }
        else if (newGameState == GameState.ingame) { }
        else if (newGameState == GameState.gameOver) { }
        currentGameState = newGameState;
    }


    public void StartGame()
    {
        SetGameState(GameState.menu);
    }

    public void GameOver()
    {
        
    }

    public void BackToMenu()
    {

    }
}
