using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance;

    Canvas mapCanvas;
    Canvas carCanvas;
    Canvas volumeCanvas;
    Canvas upgradeCanvas;

    public Slider backgroundSlider;
    public TextMeshProUGUI backgroundVolumeText;

    public Slider environmentSlider;
    public TextMeshProUGUI environmentVolumeText;

    public Slider engineSlider;
    public TextMeshProUGUI engineVolumeText;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        carCanvas = GameObject.Find("Car Canvas").GetComponent<Canvas>();
        mapCanvas = GameObject.Find("Map Canvas").GetComponent<Canvas>();
        upgradeCanvas = GameObject.Find("Upgrade Canvas").GetComponent<Canvas>();
        volumeCanvas = GetComponent<Canvas>();

        if (!PlayerPrefs.HasKey("BackgroundMusic"))
        {
            PlayerPrefs.SetInt("BackgroundMusic", 100);
            backgroundSlider.value = 100;
        }
        else
        {
            LoadBackgroundMusic();
        }

        if (!PlayerPrefs.HasKey("EnvironmentSound"))
        {
            PlayerPrefs.SetInt("EnvironmentSound", 100);
            environmentSlider.value = 100;
        }
        else
        {
            LoadEnvironmentSound();
        }

        if (!PlayerPrefs.HasKey("EngineSound"))
        {
            PlayerPrefs.SetInt("EngineSound", 100);
            engineSlider.value = 100;
        }
        else
        {
            LoadEngineSound();
        }
    }
    public void BackButton()
    {
        if (carCanvas.enabled && !mapCanvas.enabled && !upgradeCanvas.enabled)
        {
            carCanvas.enabled = true;
        }
        if (mapCanvas.enabled && !carCanvas.enabled && !upgradeCanvas.enabled)
        {
            mapCanvas.enabled = true;
        }
        if(upgradeCanvas.enabled && !mapCanvas.enabled && !carCanvas.enabled)
        {
            upgradeCanvas.enabled=true;
        }
        volumeCanvas.enabled = false;
    }
    public void LoadBackgroundMusic()
    {
        float volumeValue = PlayerPrefs.GetInt("BackgroundMusic");
        backgroundSlider.value = volumeValue;
        backgroundVolumeText.text = volumeValue.ToString();
        BackgroundMusic.instance.audioSource.volume = volumeValue/100f;
    }
    public void SetBackgroundMusic()
    {
        int sliderValue = (int)backgroundSlider.value;
        PlayerPrefs.SetInt("BackgroundMusic", sliderValue);
        BackgroundMusic.instance.audioSource.volume = sliderValue/100f;
        backgroundVolumeText.text = sliderValue.ToString();
    }

    public void LoadEnvironmentSound()
    {
        float volumeValue = PlayerPrefs.GetInt("EnvironmentSound");
        environmentSlider.value = volumeValue;
        environmentVolumeText.text = volumeValue.ToString();
        SoundManager.instance.environmentSound.volume = volumeValue / 100f;
    }
    public void SetEnvironmentSound()
    {
        int sliderValue = (int)environmentSlider.value;
        PlayerPrefs.SetInt("EnvironmentSound", sliderValue);
        SoundManager.instance.environmentSound.volume = sliderValue / 100f;
        environmentVolumeText.text = sliderValue.ToString();
    }

    public void LoadEngineSound()
    {
        float volumeValue = PlayerPrefs.GetInt("EngineSound");
        engineSlider.value = volumeValue;
        engineVolumeText.text = volumeValue.ToString();
        SoundManager.instance.carSound.volume = volumeValue / 100f;
    }
    public void SetEngineSound()
    {
        int sliderValue = (int)engineSlider.value;
        PlayerPrefs.SetInt("EngineSound", sliderValue);
        SoundManager.instance.carSound.volume = sliderValue / 100f;
        engineVolumeText.text = sliderValue.ToString();
    }
}
