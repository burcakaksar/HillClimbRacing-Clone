using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public LayerMask layerMask;
    public Canvas canvas;
    public CarController carController;
    private void Start()
    {
        canvas = GameObject.Find("Game Over Canvas").GetComponent<Canvas>();
        carController = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
    }
    private void Update()
    {
        if(CarController.fuel <= 0 && carController.currentSpeed == 0)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            StopCoroutine(GameOver());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zemin"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Car"));
            SoundManager.instance.PlayWithIndex(4);
            canvas.enabled = true;
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        canvas.enabled = true;
        Destroy(GameObject.FindGameObjectWithTag("Car"));
    }
}
