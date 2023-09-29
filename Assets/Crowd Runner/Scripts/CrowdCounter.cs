using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using static GameManager;

public class CrowdCounter : MonoBehaviour
{
    //public static CrowdCounter instance;

    [Header("Elements")]
    [SerializeField] private TextMeshPro crowdCounterText;
    [SerializeField] private Transform runnersParent;
    [SerializeField] private SpriteRenderer bubbleSprite;
    [SerializeField] private GameObject textMesh;

    private void Start()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameManager.GameState.Menu:
                GameIsStopped();
                break;
            case GameManager.GameState.Game:
                GameIsRunning();
                break;
            case GameManager.GameState.GameOver:
                GameIsStopped();
                break;
            case GameManager.GameState.LevelComplete:
                GameIsStopped();
                break;
        }
    }

    private void GameIsRunning()
    {
        bubbleSprite.enabled = true;
        textMesh.SetActive(true);
    }

    private void GameIsStopped()
    {
        bubbleSprite.enabled = false;
        textMesh.SetActive(false);
    }



    private void Update()
    {
        crowdCounterText.text = runnersParent.childCount.ToString();

        if(runnersParent.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
