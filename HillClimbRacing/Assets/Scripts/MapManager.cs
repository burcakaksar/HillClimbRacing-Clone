using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform carStartPos;
    public Sprite mapBackgroundSprite;
    public float mapBoundary;
    public Canvas pauseMenu;
    private bool isPause = false;

    private Vector3 endPos;
    private Image mapBackgroundImage;
    private GameObject car;
    private CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        endPos = GameObject.Find("Finish Flag Position").GetComponent<Transform>().position;
        SoundManager.instance.carSound.mute = false;
        CarSpawner();
        FollowCar();
        SetBackground();
    }
    private void Update()
    {
        CarBoundary();
        PauseMenu();
    }
    public void CarSpawner()
    {
        Instantiate(carPrefabs[MenuManager.carIndex], carStartPos.position, Quaternion.identity);
        SoundManager.instance.PlayCarSound(MenuManager.carIndex);
    }
    public void FollowCar()
    {
        car = GameObject.FindGameObjectWithTag("Car");
        virtualCamera = GameObject.Find("CM vcam").GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = car.transform;
    }
    public void SetBackground()
    {
        mapBackgroundImage = GameObject.Find("Map Background").GetComponent<Image>();
        mapBackgroundImage.sprite = mapBackgroundSprite;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CarController.fuel = 1;
        Time.timeScale = 1f;
        SoundManager.instance.carSound.mute = false;
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
        CarController.fuel = 1;
        Time.timeScale = 1f;
        SoundManager.instance.carSound.mute = true;
    }

    public void CarBoundary()
    {
        if (car != null)
        {

            car.transform.position = new Vector3(Mathf.Clamp(car.transform.position.x, mapBoundary, endPos.x), car.transform.position.y, car.transform.position.z);
        }
    }
    public void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                pauseMenu.enabled = true;
                Time.timeScale = 0f;
                SoundManager.instance.carSound.mute = true;
                isPause = true;
            }
            else
            {
                pauseMenu.enabled = false;
                Time.timeScale = 1f;
                SoundManager.instance.carSound.mute = false;
                isPause = false;
            }
            
        }
    }
    public void ResumeButton()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
        SoundManager.instance.carSound.mute = false;
    }
}
