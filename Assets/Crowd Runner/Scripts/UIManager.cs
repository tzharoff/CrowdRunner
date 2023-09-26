using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages:
///     Level Progress Bar
///     HUD/Menu Button Interactions
///     UI Elements Hide/Show
///     UI Elements - Level Text
/// </summary>

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        progressBar.value = 0;
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        levelText.text = $"Level {ChunkManager.instance.GetLevel}";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);
        gamePanel.SetActive(true);
        menuPanel.SetActive(false);
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
