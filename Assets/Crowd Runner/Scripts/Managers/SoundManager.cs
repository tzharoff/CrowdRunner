using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource buttonSound;

    private bool soundsEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDetection.onDoorsHit += PlayDoorHitSound;
        GameManager.onGameStateChanged += GameStateChangeCallback;
        Enemy.OnRunnerDie += PlayRunnerDieSound;
        SettingsManager.UpdateSoundState += SoundStateCallback;
    }



    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit -= PlayDoorHitSound;
        GameManager.onGameStateChanged -= GameStateChangeCallback;
        Enemy.OnRunnerDie -= PlayRunnerDieSound;
        SettingsManager.UpdateSoundState -= SoundStateCallback;
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
                PlayLevelCompleteSound();
                break;
            case GameManager.GameState.GameOver:
                PlayGameOverSound();
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayDoorHitSound()
    {
        if (!soundsEnabled)
        {
            return;
        }

        doorHitSound.Play();
    }

    private void PlayRunnerDieSound()
    {
        if (!soundsEnabled)
        {
            return;
        }

        runnerDieSound.Play();
    }

    private void PlayLevelCompleteSound()
    {
        if (!soundsEnabled)
        {
            return;
        }

        levelCompleteSound.Play();
    }

    private void PlayGameOverSound()
    {
        if (!soundsEnabled)
        {
            return;
        }

        gameOverSound.Play();
    }

    private void SoundStateCallback(bool newState)
    {
        soundsEnabled = newState;
    }

}
