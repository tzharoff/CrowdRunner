using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float roadWidth;

    [Header("Control")]
    [SerializeField] private float slideSpeed;

    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    private bool canMove;


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
        GameManager.onGameStateChanged += GameStateChangedCalled;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCalled;
    }

    private void GameStateChangedCalled(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Menu:
                break;
            case GameManager.GameState.Game:
                MoveEnable();
                break;
            case GameManager.GameState.LevelComplete:
                MoveDisable();
                break;
            case GameManager.GameState.GameOver:
                MoveDisable();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }


        MoveForward();
        ManageControl();

    }

    private void MoveEnable()
    {
        canMove = true;
        playerAnimator.Run();
    }

    private void MoveDisable()
    {
        canMove = false;
        playerAnimator.Idle();
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        } else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;

            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;
            position.x = Mathf.Clamp(
                position.x,
                -roadWidth / 2 + crowdSystem.GetRadius(),
                roadWidth / 2 - crowdSystem.GetRadius());
            transform.position = position;
        }
    }

}
