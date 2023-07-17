using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    public TMP_Text coinText;
    public int currentCoins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 0);
            coinText.text = PlayerPrefs.GetInt("Coin").ToString();
        }
        else
        {
            GetCoin(coinText);
        }
    }

    public void GetCoin(TMP_Text text)
    {
        text.text = PlayerPrefs.GetInt("Coin").ToString();
        currentCoins = PlayerPrefs.GetInt("Coin");
    }

    public void SetCoin(int coinValue)
    {
        currentCoins += coinValue;
        PlayerPrefs.SetInt("Coin", currentCoins);
        GetCoin(coinText);
    }
}