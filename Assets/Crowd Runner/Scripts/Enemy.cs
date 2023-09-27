using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum EnemyState { Idle, Running }


    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float killRange = 0.1f;
    [SerializeField] private EnemyState enemyState;
    [SerializeField] private Transform targetRunner;

    public static Action OnRunnerDie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                SearchForTarget();
                break;
            case EnemyState.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            //Debug.Log($"number of colliders seen: {colliders.Length}");
            if (colliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                {
                    continue;
                }

                runner.SetTarget(gameObject);
                targetRunner = runner.transform;

                StartRunningTowardsTarget();
                break;
            }
        }
    }

    private void StartRunningTowardsTarget()
    {
        enemyState = EnemyState.Running;
        GetComponent<Animator>().Play("Run");
    }

    private void RunTowardsTarget()
    {
        if(targetRunner == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);

        if(Vector3.Distance(transform.position, targetRunner.position) < killRange)
        {
            OnRunnerDie?.Invoke();
            //Debug.Log($"{targetRunner.name} is killed by {gameObject.name}");
            targetRunner.SetParent(null);
            Destroy(targetRunner.gameObject);
            transform.SetParent(null);
            Destroy(gameObject);
        }

    }


}
