using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreCounter : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;
    public static int point = 0;
    public int scoreMultiplier;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float distance;

    private bool canCount = false;
    private Vector3 startPos;
    public CarController carController;
    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI pointText;
    private void Awake()
    {
        startPos = GameObject.Find("Start Car Position").GetComponent<Transform>().position;
        startPos = new Vector3(startPos.x + 1, startPos.y, startPos.z);
        highScoreText = GameObject.Find("High Score Text").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        pointText = GameObject.Find("Point Text").GetComponent<TextMeshProUGUI>();
        pointText.enabled = false;

        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        else
        {
            LoadHighScore();
        }
        score = 0;
    }
    private void Start()
    {
        carController = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Car") != null)
        {
            SetScore();
        }
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score : " + highScore.ToString();
    }

    public void SetScore()
    {
        if (!carController.IsGround() && (startPos.x < carController.carPos.x))
        {
            point += Mathf.RoundToInt(Time.deltaTime * scoreMultiplier);
            if (point > 0 && !CanCount())
            {
                StartCoroutine(PointText());
            }
        }
        else
        {
            pointText.enabled = false;
            score += point;
            scoreText.text = "Your Score : " + score.ToString();
            point = 0;
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }
    }

    IEnumerator PointText()
    {
        pointText.enabled = true;
        pointText.text = "+" + point + " Points!";
        yield return new WaitForSeconds(1.5f);
    }

    private bool CanCount()
    {
        return Physics2D.Raycast(carController.gameObject.transform.position, Vector2.down, distance, layerMask);
        
    }
}
