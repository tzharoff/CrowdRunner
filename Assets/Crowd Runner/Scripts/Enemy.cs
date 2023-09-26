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
    private EnemyState enemyState;
    [SerializeField] private Transform targetRunner;

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
            if (colliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                {
                    continue;
                }

                runner.SetTarget();
                targetRunner = runner.transform;

                StartRunningTowardsTarget();
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
            targetRunner.SetParent(null);
            Destroy(targetRunner.gameObject);
            transform.SetParent(null);
            Destroy(gameObject);
        }

    }


}
