using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public static Action CoinsUpdate;

    [Header("Coin Text")]
    [SerializeField] private TMP_Text coinText;

    [Header("Variables")]
    [SerializeField] private int skinIndex;
    [SerializeField] private int runnerCount;

    private int coins;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }

        coins = PlayerPrefs.GetInt("coins", 0);
        UpdateCoinText();
    }

    // Start is called before the first frame update
    void Start()
    {
        ShopManager.onSkinSelected += OnSkinSelectedCallback;

        int savedSkin = PlayerPrefs.GetInt("skinIndex", 0);
    }

    private void OnDestroy()
    {
        ShopManager.onSkinSelected += OnSkinSelectedCallback;
    }

    private void OnSkinSelectedCallback(int skinIndex)
    {
        this.skinIndex = skinIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCoinText()
    {
        coinText.text = coins.ToString();
        CoinsUpdate?.Invoke();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText();
        PlayerPrefs.SetInt("coins", coins);
    }

    public void UseCoins(int amount)
    {
        coins -= amount;
        UpdateCoinText();
        PlayerPrefs.SetInt("coins", coins);
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetSkinIndex()
    {
        return skinIndex;
    }

    public int GetRunnerCount()
    {
        return runnerCount;
    }
}
