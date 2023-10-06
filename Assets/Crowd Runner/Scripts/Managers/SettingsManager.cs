using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Sprite optionsOnSprite;
    [SerializeField] private Sprite optionsOffSprite;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image hapticsButtonImage;

    public static Action<bool> UpdateSoundState;
    public static Action<bool> UpdateHapticState;

    private bool soundState = true;
    private bool hapticState = true;

    private void Awake()
    {
        soundState = PlayerPrefs.GetInt("sounds", 1) == 1;
        hapticState = PlayerPrefs.GetInt("haptics", 1) == 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        if (soundState)
        {
            DisableSounds();
        }
        else
        {
            EnableSound();
        }

        if (hapticState)
        {
            DisableHaptics();
        } else
        {
            EnableHaptics();
        }
    }


    public void ChangeSoundState()
    {
        if (soundState)
        {
            DisableSounds();
        } else
        {
            EnableSound();
        }

        soundState = !soundState;

        PlayerPrefs.SetInt("sounds", soundState ? 1 : 0);
    }

    private void EnableSound()
    {
        soundsButtonImage.sprite = optionsOnSprite;
        UpdateSoundState?.Invoke(true);

    }

    private void DisableSounds()
    {
        soundsButtonImage.sprite = optionsOffSprite;
        UpdateSoundState?.Invoke(false);
    }


    public void ChangeHapticState()
    {
        if (hapticState)
        {
            DisableHaptics();
        }
        else
        {
            EnableHaptics();
        }

        hapticState = !hapticState;
        PlayerPrefs.SetInt("haptics", hapticState ? 1 : 0);
    }

    private void EnableHaptics()
    {
        hapticsButtonImage.sprite = optionsOffSprite;
        UpdateHapticState?.Invoke(false);
    }

    private void DisableHaptics()
    {
        hapticsButtonImage.sprite = optionsOnSprite;
        UpdateHapticState?.Invoke(true);
    }

}
