using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public enum BonusType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}

public class Doors : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField] private Collider doorCollider;

    [Header("Right Door Elements")]
    [SerializeField] private SpriteRenderer rightDoorRenderer;
    [SerializeField] private TextMeshPro rightDoorText;

    [Header("Left Door Elements")]
    [SerializeField] private SpriteRenderer leftDoorRenderer;
    [SerializeField] private TextMeshPro leftDoorText;

    [Header("Settings - Right Door")]
    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusAmount;

    [Header("Settings - Left Door")]
    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusAmount;


    [Header("Settings")]
    [SerializeField] Color bonusColor;
    [SerializeField] Color penaltyColor;




    // Start is called before the first frame update
    void Start()
    {
        ConfigureDoors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConfigureDoors()
    {
        //configure right door
        switch (rightDoorBonusType)
        {
            case BonusType.Addition:
                SetDoorColor(bonusColor, true);
                SetDoorText($"+{rightDoorBonusAmount}", true);
                break;
            case BonusType.Subtraction:
                SetDoorColor(penaltyColor, true);
                SetDoorText($"-{rightDoorBonusAmount}", true);
                break;
            case BonusType.Multiplication:
                SetDoorColor(bonusColor, true);
                SetDoorText($"x{rightDoorBonusAmount}", true);
                break;
            case BonusType.Division:
                SetDoorColor(penaltyColor, true);
                SetDoorText($"÷{rightDoorBonusAmount}", true);
                break;
        }

        //configure left door
        switch (leftDoorBonusType)
        {
            case BonusType.Addition:
                SetDoorColor(bonusColor, false);
                SetDoorText($"+{leftDoorBonusAmount}", false);
                break;
            case BonusType.Subtraction:
                SetDoorColor(penaltyColor, false);
                SetDoorText($"-{leftDoorBonusAmount}", false);
                break;
            case BonusType.Multiplication:
                SetDoorColor(bonusColor, false);
                SetDoorText($"x{leftDoorBonusAmount}", false);
                break;
            case BonusType.Division:
                SetDoorColor(penaltyColor, false);
                SetDoorText($"÷{leftDoorBonusAmount}", false);
                break;
        }
    }

    private void SetDoorColor(Color newColor, bool rightDoor)
    {
        if (rightDoor)
        {
            rightDoorRenderer.color = newColor;
        } else
        {
            leftDoorRenderer.color = newColor;
        }
        
    }

    private void SetDoorText(string newText, bool rightDoor)
    {
        if (rightDoor)
        {
            rightDoorText.text = newText;
        } else
        {
            leftDoorText.text = newText;
        }
    }


    public int GetBonus(float position)
    {
        //greater than 0 is right, less than 0 is left
        if(position > 0)
        {
            return rightDoorBonusAmount;
        }

        return leftDoorBonusAmount;
    }

    public BonusType GetBonusType(float position)
    {
        //greater than 0 is right, less than 0 is left
        if (position > 0)
        {
            return rightDoorBonusType;
        }

        return leftDoorBonusType;
        
    }

    public void DisableCollision()
    {
        doorCollider.enabled = false;
    }
}
