using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    [SerializeField] GameObject mapPurchasePart;
    [SerializeField] GameObject carPurchasePart;
    [SerializeField] TextMeshProUGUI mapPurchaseValueText;
    [SerializeField] TextMeshProUGUI carPurchaseValueText;
    [SerializeField] int[] mapPurchaseValues;
    [SerializeField] int[] carPurchaseValues;


    private MenuManager menuManager;
    private int wallet;
    private int purchaseValue;
    public bool canCarStart = true;
    public bool canMapStart = true;

    private void Awake()
    {
        menuManager = GetComponent<MenuManager>();
        wallet = PlayerPrefs.GetInt("Coin");
        
        foreach (string maps in menuManager.mapNames)
        {
            if (!PlayerPrefs.HasKey(maps))
            {
                PlayerPrefs.SetInt(maps, 0);
            }
        }
        foreach(string cars in menuManager.carNames)
        {
            if (!PlayerPrefs.HasKey(cars))
            {
                PlayerPrefs.SetInt(cars, 0);
            }
        }
        PlayerPrefs.SetInt(menuManager.mapNames[0], 1);
        PlayerPrefs.SetInt(menuManager.carNames[0], 1);
        CanBuyMap(0);
        CanBuyCar(0);
    }
    public void PurchaseButton()
    {
        if (purchaseValue <= wallet && menuManager.mapIsActive)
        {
            PlayerPrefs.SetInt("Coin", wallet - purchaseValue);
            PlayerPrefs.SetInt(menuManager.mapNames[MenuManager.mapIndex], 1);
            SoundManager.instance.PlayWithIndex(3);
            CanBuyMap(MenuManager.mapIndex);
            menuManager.MenuSetCoin();
        }
        if (purchaseValue <= wallet && menuManager.carIsActive)
        {
            PlayerPrefs.SetInt("Coin", wallet - purchaseValue);
            PlayerPrefs.SetInt(menuManager.carNames[MenuManager.carIndex], 1);
            SoundManager.instance.PlayWithIndex(3);
            CanBuyCar(MenuManager.carIndex);
            menuManager.MenuSetCoin();
        }
    }

    public  void CanBuyMap(int index)
    {
        if (PlayerPrefs.GetInt(menuManager.mapNames[index]) == 1)
        {
            mapPurchasePart.SetActive(false);
            canMapStart = true;
            menuManager.mapPageCoinText.color = Color.green;
        }
        else
        {
            mapPurchasePart.SetActive(true);
            purchaseValue = mapPurchaseValues[index];        
            mapPurchaseValueText.SetText(purchaseValue.ToString());
            canMapStart = false;
            if(purchaseValue > wallet)
            {
                menuManager.mapPageCoinText.color = Color.red;
            }
            else
            {
                menuManager.mapPageCoinText.color = Color.green;
            }
        }

    }

    public void CanBuyCar(int index)
    {
        if (PlayerPrefs.GetInt(menuManager.carNames[index]) == 1)
        {
            carPurchasePart.SetActive(false);
            canCarStart = true;
            menuManager.carPageCoinText.color = Color.green;
        }
        else
        {
            carPurchasePart.SetActive(true);
            purchaseValue = carPurchaseValues[index];
            carPurchaseValueText.SetText(purchaseValue.ToString());
            canCarStart = false;
            if(purchaseValue > wallet)
            {
                menuManager.carPageCoinText.color = Color.red;
            }
            else
            {
                menuManager.carPageCoinText.color = Color.green;
            }
            
        }
    }
}
