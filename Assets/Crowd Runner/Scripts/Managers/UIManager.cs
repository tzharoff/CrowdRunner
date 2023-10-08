using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Manages:
///     Level Progress Bar
///     HUD/Menu Button Interactions
///     UI Elements Hide/Show
///     UI Elements - Level Text
/// </summary>

public class UIManager : MonoBehaviour
{
    [Header("Panel Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;

    [Header("Elements")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        progressBar.value = 0;

        //when making builds for play
        ShowMenuPanel();

        //for debugging panels
        //ShowShopPanel();


        levelText.text = $"Level {ChunkManager.instance.GetLevel}";

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar();
    }

    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameManager.GameState.Menu:
                break;
            case GameManager.GameState.Game:
                break;
            case GameManager.GameState.GameOver:
                ShowGameOver();
                break;
            case GameManager.GameState.LevelComplete:
                ShowLevelComplete();
                break;
        }
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);
        gamePanel.SetActive(true);
        menuPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void RetryButtonPressed()
    {
        InterstitialAd.instance.ShowAd();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowMenuPanel()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void ShowGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        levelCompletePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void ShowLevelComplete()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(true);
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void ShowSettingsPannel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        settingsPanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    public void ShowShopPanel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        settingsPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState())
        {
            return;
        }
        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ;
        progressBar.value = progress;

    }


}
