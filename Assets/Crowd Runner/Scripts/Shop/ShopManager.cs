using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    [Header("Skins")]
    [SerializeField] private Sprite[] skins;

    [Header("Elements")]
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private Button purchaseButton;

    [Header("Pricing")]
    [SerializeField] private int skinPrice;
    [SerializeField] private TMP_Text priceText;

    public static Action<int> onSkinSelected;

    private void Awake()
    {
        priceText.text = $"{skinPrice}";
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        RewardedAdButton.onRewardGranted += OnRewardGrantedCallback;
        Debug.Log("Player prefs cleared in ShopManager.cs");
        PlayerPrefs.DeleteAll();

        ConfigureButtons();

        yield return null;
        UnlockSkin(0);
        SelectSkin(GetLastSelectedSkin());
    }

    private void OnEnable()
    {
        UpdatePurchaseButton();
        DataManager.CoinsUpdate += UpdatePurchaseButton;
    }

    private void OnDisable()
    {
        DataManager.CoinsUpdate -= UpdatePurchaseButton;
        RewardedAdButton.onRewardGranted -= OnRewardGrantedCallback;
    }

    private void OnRewardGrantedCallback()
    {
        int saveSkinPrice = skinPrice;
        skinPrice = 0;
        PurchaseSkin();
        skinPrice = saveSkinPrice;
    }

    private void ConfigureButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt($"skinButton{0}", 0) == 1;

            //Debug.Log($"{skinButtons[i].name} unlocked value is {unlocked}");
            skinButtons[i].Configure(skins[i], unlocked);
            skinButtons[i].Deselect();
            skinButtons[i].Index = i;

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }

        UpdatePurchaseButton();

    }

    public void UnlockSkin(int index)
    {
        PlayerPrefs.SetInt($"skinButton{index}", 1);
        skinButtons[index].Unlock();
    }

    private void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            skinButtons[i].Deselect();
        }
        skinButtons[skinIndex].Select();

        onSkinSelected?.Invoke(skinIndex);
        SaveLastSelectedSkin(skinIndex);
    }

    public void PurchaseSkin()
    {
        List<SkinButton> skinButtonsList = new List<SkinButton>();

        for (int i = 0; i < skinButtons.Length; i++)
        {
            Debug.Log($"{skinButtons[i].name} unlocked value is {skinButtons[i].UnlockState}");

            if (!skinButtons[i].UnlockState)
            {
                skinButtonsList.Add(skinButtons[i]);
            }
        }

        //all skins unlocked
        if(skinButtonsList.Count <= 0)
        {
            return;
        }

        SkinButton randomSkinButton = skinButtonsList[Random.Range(0, skinButtonsList.Count)];

        UnlockSkin(randomSkinButton.Index);
        SelectSkin(randomSkinButton.Index);

        DataManager.instance.UseCoins(skinPrice);

        //updating the coins calls this, we don't need it, but keep it around JUST IN CASE
        //UpdatePurchaseButton();

    }

    private void UpdatePurchaseButton()
    {
        //Debug.Log($"Coins: {DataManager.instance.GetCoins()},skinprice: {skinPrice} ");
        if(DataManager.instance.GetCoins() < skinPrice)
        {
            purchaseButton.interactable = false;
            return;
        }

        purchaseButton.interactable = true;
    }

    private int GetLastSelectedSkin()
    {
        return PlayerPrefs.GetInt("lastSelectedSkin", 0);
    }

    private void SaveLastSelectedSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("lastSelectedSkin", skinIndex);
    }
}
