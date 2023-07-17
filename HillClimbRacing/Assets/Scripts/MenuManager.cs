using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image imageBox;
    public TextMeshProUGUI textBox;
    private BuyManager buyManager;

    [Header("Menu Buttons")]
    [SerializeField] Button goCarPageButton;
    [SerializeField] Button goUpgradePageButton;

    private Canvas mapCanvas;
    private Canvas carCanvas;
    private Canvas upgradeCanvas;

    [Header("Map Settings")]
    public Sprite[] maps;
    public string[] mapNames = { "COUNTRYSIDE", "HIGHWAY", "FOREST" };
    public static int mapIndex = 0;
    public bool mapIsActive = false;

    [Header("Car Settings")]
    public Sprite[] cars;
    public string[] carNames = { "JEEP", "RACE CAR", "BIKE" };
    public static int carIndex = 0;
    public bool carIsActive = false;

    [Header("Top Bar Settings")]
    public TextMeshProUGUI mapPageCoinText;
    public TextMeshProUGUI carPageCoinText;
    public TextMeshProUGUI upgradePageCoinText;
    private void Awake()
    {
        carCanvas = GameObject.Find("Car Canvas").GetComponent<Canvas>();
        mapCanvas = GameObject.Find("Map Canvas").GetComponent<Canvas>();
        upgradeCanvas = GameObject.Find("Upgrade Canvas").GetComponent<Canvas>();
        buyManager = GameObject.Find("Menu Manager").GetComponent<BuyManager>();
        MenuSetCoin();
    }

    private void Update()
    {
        ActiveCanvasFinder();
        CanvasButtonChecker();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        if (buyManager.canMapStart && buyManager.canCarStart)
        {
            SceneManager.LoadScene(mapIndex + 1);
            CarController.fuel = 1;
        }   
    }
    public void NextButton()
    {
        if (mapIsActive)
        {
            mapIndex = ImageIndexSetter(++mapIndex, maps.Length);
            buyManager.CanBuyMap(mapIndex);
            imageBox.sprite = maps[mapIndex];
        }

        if (carIsActive)
        {
            carIndex = ImageIndexSetter(++carIndex, cars.Length);
            buyManager.CanBuyCar(carIndex);
            imageBox.sprite = cars[carIndex];
        }
    }
    public void PrevButton()
    {
        if (mapIsActive)
        {
            mapIndex = ImageIndexSetter(--mapIndex, maps.Length);
            buyManager.CanBuyMap(mapIndex);
            imageBox.sprite = maps[mapIndex];
        }

        if (carIsActive)
        {
            carIndex = ImageIndexSetter(--carIndex, cars.Length);
            buyManager.CanBuyCar(carIndex);
            imageBox.sprite = cars[carIndex];
        }
    }

    public int ImageIndexSetter(int index, int arrayLength)
    {
        if (index == arrayLength)
        {
            return 0;
        }
        if (index == -1)
        {
            return arrayLength - 1;
        }
        return index;
    }
    public void ActiveCanvasFinder()
    {
        if (mapCanvas.enabled)
        {
            mapIsActive = true;
            carIsActive = false;
            imageBox = GameObject.Find("Map Image").GetComponent<Image>();
            imageBox.sprite = maps[mapIndex];
            textBox = GameObject.Find("Map Name Text").GetComponent<TextMeshProUGUI>();
            textBox.text = mapNames[mapIndex];

        }
        if (carCanvas.enabled)
        {
            mapIsActive = false;
            carIsActive = true;
            imageBox = GameObject.Find("Car Image").GetComponent<Image>();
            imageBox.sprite = cars[carIndex];
            textBox = GameObject.Find("Car Name Text").GetComponent<TextMeshProUGUI>();
            textBox.text = carNames[carIndex];
        }
    }

    public void MenuSetCoin()
    {
        int coinValue = PlayerPrefs.GetInt("Coin");
        mapPageCoinText.text = coinValue.ToString();
        carPageCoinText.text = coinValue.ToString();
        upgradePageCoinText.text = coinValue.ToString();
    }

    public void CanvasButtonChecker()
    {
        if (!buyManager.canMapStart)
        {
            goCarPageButton.interactable = false;
        }
        else
        {
            goCarPageButton.interactable = true;
        }

        if (!buyManager.canCarStart)
        {
            goUpgradePageButton.interactable = false;
        }
        else
        {
            goUpgradePageButton.interactable = true;
        }
    }
}
