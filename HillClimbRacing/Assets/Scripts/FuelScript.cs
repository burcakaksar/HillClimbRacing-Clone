using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        CarController.fuel = 1;
        SoundManager.instance.PlayWithIndex(2);
    }
}
