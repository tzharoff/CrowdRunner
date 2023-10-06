using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnersParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Run()
    {
        //Debug.Log($"there are {runnersParent.childCount} children");
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform runnerChild = runnersParent.GetChild(i);
            runnerChild.gameObject.name = $"{runnerChild.gameObject.name} {i}";
            //Debug.Log($"runnerchild's name {runnerChild.gameObject.name}");
            Animator runnerAnimator = runnerChild.gameObject.GetComponent<Runner>().GetAnimator();
            runnerAnimator.Play(Animator.StringToHash("Run"));
        }
    }

    public void Idle()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform runnerChild = runnersParent.GetChild(i);
            Animator runnerAnimator = runnerChild.gameObject.GetComponent<Runner>().GetAnimator();
            runnerAnimator.Play(Animator.StringToHash("Idle"));
        }
    }
}
