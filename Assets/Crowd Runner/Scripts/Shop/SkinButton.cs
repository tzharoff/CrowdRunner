using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Button button;
    [SerializeField] private Image skinImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selector;

    private bool unlockState;
    private int _index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Configure(Sprite skinSprite, bool unlockState)
    {
        skinImage.sprite = skinSprite;
        skinImage.preserveAspect = true;

        this.unlockState = unlockState;
        if (unlockState)
        {
            Unlock();
        } else
        {
            Lock();
        }

    }

    public void Unlock()
    {
        button.interactable = true;
        skinImage.gameObject.SetActive(true);
        lockImage.SetActive(false);

        unlockState = true;
    }

    public void Lock()
    {
        button.interactable = false;
        skinImage.gameObject.SetActive(false);
        lockImage.SetActive(true);

        unlockState = false;
    }

    public void Select()
    {
        selector.SetActive(true);
    }

    public void Deselect()
    {
        selector.SetActive(false);
    }

    public Button GetButton()
    {
        return button;
    }

    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
        }
    }

    public bool UnlockState { get { return unlockState; } set { unlockState = value; } }
}
