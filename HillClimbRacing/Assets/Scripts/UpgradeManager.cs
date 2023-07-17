using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private Canvas upgradeCanvas;
    private CarPartSO currentCar;
    private int currentCarIndex;
    private int wallet;
    private MenuManager menuManager;
    private bool[] canUpgrades;
    private TextMeshProUGUI upgradeCoinText;
    [SerializeField] CarPartSO[] scriptableObjects;
    [SerializeField] float[] prices;
    [SerializeField] TextMeshProUGUI[] levelText, priceText;
    [SerializeField] Button[] buttons;

    [Header("Upgrade Values")]
    [SerializeField] float engine_SPEEDVALUE;
    [SerializeField] float suspension_DAMPINGVALUE;
    [SerializeField] float suspension_FREQUENCYVALUE;
    [SerializeField] float tires_FUELCONSUMPTIONVALUE;
    [SerializeField] float airControl_ROTATIONVALUE;
    #region Singleton
    public static UpgradeManager instance;

    private void Awake()
    {
        canUpgrades = new bool[4];
        menuManager = GetComponent<MenuManager>();
        currentCar = scriptableObjects[currentCarIndex];
        upgradeCanvas = GameObject.Find("Upgrade Canvas").GetComponent<Canvas>();
        upgradeCoinText = menuManager.upgradePageCoinText;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SetCarUpgrades();
    }
    #endregion
    private void Update()
    {
        if (upgradeCanvas.enabled)
        {

            SetCarUpgrades();
        }
    }
    public void SetCarUpgrades()
    {
        wallet = PlayerPrefs.GetInt("Coin");
        currentCarIndex = MenuManager.carIndex;
        currentCar = scriptableObjects[currentCarIndex];
        int index = 0;
        foreach (int level in currentCar.levels)
        {
            if (level == 5)
            {
                levelText[index].SetText("MAX");
                priceText[index].SetText("LEVEL");
            }
            else
            {
                levelText[index].SetText("LEVEL : " + level.ToString());
                priceText[index].SetText(prices[level - 1].ToString());
                canUpgrades[index] = true;
                buttons[index].interactable = true;
            }

            if (level > prices.Length)
            {
                canUpgrades[index] = false;
                buttons[index].interactable = false;
            }
            index++;
        }
    }

    public void UpgradeEngine()
    {
        if (canUpgrades[0] && (wallet >= prices[currentCar.engineLevel - 1]))
        {
            int upgradeValue = (int)(PlayerPrefs.GetInt("Coin") - prices[currentCar.engineLevel - 1]);
            PlayerPrefs.SetInt("Coin", upgradeValue);
            SoundManager.instance.PlayWithIndex(3);
            menuManager.MenuSetCoin();
            currentCar.speed += engine_SPEEDVALUE;
            currentCar.maxSpeed += (engine_SPEEDVALUE / 2);
            currentCar.levels[0] = ++currentCar.engineLevel;
            SetCarUpgrades();
        }
        else
        {
            StartCoroutine(BlinkCoinText());
        }

    }
    public void UpgradeSuspension()
    {
        if (canUpgrades[1] && (wallet >= prices[currentCar.suspensionLevel - 1]))
        {
            int upgradeValue = (int)(PlayerPrefs.GetInt("Coin") - prices[currentCar.suspensionLevel - 1]);
            PlayerPrefs.SetInt("Coin", upgradeValue);
            SoundManager.instance.PlayWithIndex(3);
            menuManager.MenuSetCoin();
            currentCar.dampingValue -= suspension_DAMPINGVALUE;
            currentCar.frequencyValue += suspension_FREQUENCYVALUE;
            currentCar.levels[1] = ++currentCar.suspensionLevel;
            SetCarUpgrades();
        }
        else
        {
            StartCoroutine(BlinkCoinText());
        }

    }
    public void UpgradeTires()
    {
        if (canUpgrades[2] && (wallet >= prices[currentCar.tiresLevel - 1]))
        {
            int upgradeValue = (int)(PlayerPrefs.GetInt("Coin") - prices[currentCar.tiresLevel - 1]);
            PlayerPrefs.SetInt("Coin", upgradeValue);
            SoundManager.instance.PlayWithIndex(3);
            menuManager.MenuSetCoin();
            currentCar.fuelConsumption -= tires_FUELCONSUMPTIONVALUE;
            currentCar.levels[2] = ++currentCar.tiresLevel;
            SetCarUpgrades();
        }
        else
        {
            StartCoroutine(BlinkCoinText());
        }

    }
    public void UpgradeAirControl()
    {
        if (canUpgrades[3] && (wallet >= prices[currentCar.airControlLevel - 1]))
        {
            int upgradeValue = (int)(PlayerPrefs.GetInt("Coin") - prices[currentCar.airControlLevel - 1]);
            PlayerPrefs.SetInt("Coin", upgradeValue);
            SoundManager.instance.PlayWithIndex(3);
            menuManager.MenuSetCoin();
            currentCar.rotationValue += airControl_ROTATIONVALUE;
            currentCar.levels[3] = ++currentCar.airControlLevel;
            SetCarUpgrades();
        }
        else
        {
            StartCoroutine(BlinkCoinText());
        }
    }

    IEnumerator BlinkCoinText()
    {
        Color originalColor = Color.white;
        Color redColor = Color.red;

        for (int i = 0; i < 4; i++)
        {
            upgradeCoinText.color = redColor;
            yield return new WaitForSeconds(0.40f);

            upgradeCoinText.color = originalColor;
            yield return new WaitForSeconds(0.40f);
        }
    }
}
