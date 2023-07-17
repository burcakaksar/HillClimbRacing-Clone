using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.enabled = false;
        StartBlinking();
    }

    private void Update()
    {
        LowFuelText();
    }

    public void LowFuelText()
    {
        if (CarController.fuel > 0 && CarController.fuel < 0.25)
        {
            text.enabled = true;
        }
        if (CarController.fuel <= 0)
        {
            text.enabled = false;
            StopBlinking();
        }
        if(CarController.fuel >= 0.25)
        {
            text.enabled = false;
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch (text.color.a.ToString())
            {
                case "0":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        StopCoroutine(Blink());
        StartCoroutine(Blink());

    }

    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}
