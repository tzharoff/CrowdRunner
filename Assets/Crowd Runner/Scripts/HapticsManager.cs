using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

public class HapticsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerDetection.onDoorsHit += PlayDoorHitSound;
    }

    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit -= PlayDoorHitSound;
    }

    private void PlayDoorHitSound()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
    }

}
