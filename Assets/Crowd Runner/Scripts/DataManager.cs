using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("Coin Text")]
    [SerializeField] private TMP_Text coinText;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCoinText()
    {
        coinText.text = coins.ToString();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText();
        PlayerPrefs.SetInt("coins", coins);
    }
}
