using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowdCounter : MonoBehaviour
{
    public static CrowdCounter instance;

    [Header("Elements")]
    [SerializeField] private TextMeshPro crowdCounterText;

    private int _crowdCount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            if(instance != this)
            {
                Destroy(this);
            }
        }
    }

    public int CrowdCount
    {
        get { return _crowdCount; }
        set { _crowdCount = value; SetText(value); }
    }

    private void SetText(int crowdCount)
    {
        crowdCounterText.text = crowdCount.ToString();
    }
}
