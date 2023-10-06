using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnersParent;

    // Start is called before the first frame update
    void Start()
    {
        ShopManager.onSkinSelected += SelectSkin;
        SelectSkin(DataManager.instance.GetSkinIndex());
    }

    private void OnDestroy()
    {
        ShopManager.onSkinSelected -= SelectSkin;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            int randomSkin = Random.Range(0, DataManager.instance.GetRunnerCount());
            SelectSkin(randomSkin);
        }
    }

    public void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            runnersParent.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);
        }
        PlayerPrefs.SetInt("skinIndex", skinIndex);
    }
}
