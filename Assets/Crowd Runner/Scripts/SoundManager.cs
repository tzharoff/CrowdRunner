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


    // Start is called before the first frame update
    void Start()
    {
        PlayerDetection.onDoorsHit += PlayDoorHitSound;
        GameManager.onGameStateChanged += GameStateChangeCallback;
        Enemy.OnRunnerDie += PlayRunnerDieSound;
    }

    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit -= PlayDoorHitSound;
        GameManager.onGameStateChanged -= GameStateChangeCallback;
        Enemy.OnRunnerDie -= PlayRunnerDieSound;
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
        doorHitSound.Play();
    }

    private void PlayRunnerDieSound()
    {
        runnerDieSound.Play();
    }

    private void PlayLevelCompleteSound()
    {
        levelCompleteSound.Play();
    }

    private void PlayGameOverSound()
    {
        gameOverSound.Play();
    }

}
