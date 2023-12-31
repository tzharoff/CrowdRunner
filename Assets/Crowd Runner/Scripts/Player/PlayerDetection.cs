using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Handles Player Door and Finish Line interactions
/// </summary>

public class PlayerDetection : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;

    public static Action onDoorsHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectDoors();
    }

    private void DetectDoors()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, crowdSystem.GetRadius());

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Doors door))
            {
                door.DisableCollision();
                BonusType bonusType = door.GetBonusType(transform.position.x);
                int bonusAmount = door.GetBonus(transform.position.x);
                onDoorsHit?.Invoke();
                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }

            if (colliders[i].CompareTag("Finish"))
            {
                //Debug.Log("Finish Line");
                int currentLevel = PlayerPrefs.GetInt("Level");
                currentLevel++;
                PlayerPrefs.SetInt("Level", currentLevel);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);
                colliders[i].gameObject.SetActive(false);
            }

            if (colliders[i].CompareTag("Coin"))
            {
                Destroy(colliders[i].gameObject);
                DataManager.instance.AddCoins(1);
            }
        }
    }

}
