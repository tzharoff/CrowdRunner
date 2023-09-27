using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [Description("Is the Runner targeted?")]
    [SerializeField] private bool isTarget;
    [SerializeField] private GameObject targetedBy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget()
    {
        isTarget = true;
    }
    public void SetTarget(GameObject targeter)
    {
        Debug.Log($"{gameObject.name} is targeted by {targeter.name}");
        targetedBy = targeter;
        isTarget = true;
    }

    public bool IsTarget()
    {
        return isTarget;
    }
}
