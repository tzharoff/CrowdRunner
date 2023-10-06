using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState { Menu, Game, LevelComplete, GameOver }

    public static Action<GameState> onGameStateChanged;

    private GameState gameState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GameState newGameState)
    {
        gameState = newGameState;
        onGameStateChanged?.Invoke(newGameState);

        //Debug.Log($"Game State set to {newGameState}");
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
