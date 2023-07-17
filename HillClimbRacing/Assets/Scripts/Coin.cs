using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        CoinCounter.instance.SetCoin(value);
        SoundManager.instance.PlayWithIndex(1);
    }
}