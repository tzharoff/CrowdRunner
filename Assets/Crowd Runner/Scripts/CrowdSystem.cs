using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private GameObject runnerPrefab;
    [SerializeField] private Transform runnersParent;

    [Header("Settings")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsGameState())
        {
            return;
        }

        PlaceRunners();

        if(runnersParent.childCount <= 0)
        {
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
        }
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }

    public float GetRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount) {
        switch (bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;
            case BonusType.Subtraction:
                RemoveRunners(bonusAmount);
                break;
            case BonusType.Multiplication:
                int amountMultiplication = (runnersParent.childCount * bonusAmount) - runnersParent.childCount;
                AddRunners(amountMultiplication);
                break;
            case BonusType.Division:
                int amountDivision = runnersParent.childCount - (runnersParent.childCount / bonusAmount);
                RemoveRunners(amountDivision);
                break;
        }
    }

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnersParent);
        }
        playerAnimator.Run();
    }

    public void RemoveRunners(int amount)
    {
        if(amount > runnersParent.childCount)
        {
            amount = runnersParent.childCount;
        }

        for (int i = runnersParent.childCount - 1; i >= runnersParent.childCount - amount; i--)
        {
            Transform runnerToDestroy = runnersParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }

}
