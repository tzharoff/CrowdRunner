using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

public class HapticsManager : MonoBehaviour
{
    private bool hapticsEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDetection.onDoorsHit += PlayDoorHitHaptics;
        GameManager.onGameStateChanged += GameStateChangeCallback;
        SettingsManager.UpdateHapticState += HapticStateCallback;
    }



    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit -= PlayDoorHitHaptics;
        GameManager.onGameStateChanged -= GameStateChangeCallback;
        SettingsManager.UpdateHapticState -= HapticStateCallback;
    }


    private void GameStateChangeCallback(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Menu:
                break;
            case GameManager.GameState.Game:
                break;
            case GameManager.GameState.LevelComplete:
                PlayLevelCompleteHaptics();
                break;
            case GameManager.GameState.GameOver:
                PlayGameOverHaptics();
                break;
        }
    }

    private void PlayDoorHitHaptics()
    {
        if (!hapticsEnabled)
        {
            return;
        }

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
    }


    private void PlayGameOverHaptics()
    {
        if (!hapticsEnabled)
        {
            return;
        }

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);
    }

    private void PlayLevelCompleteHaptics()
    {
        if (!hapticsEnabled)
        {
            return;
        }

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
    }

    private void HapticStateCallback(bool newState)
    {
        hapticsEnabled = newState;
    }
}
