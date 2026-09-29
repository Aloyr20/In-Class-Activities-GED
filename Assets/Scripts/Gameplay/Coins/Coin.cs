using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject);
        FindObjectOfType<CoinCounter>().AddCoin(1);
        ScoreCounter.Instance.AddScore(100);
        AudioManager.Instance.Play("Coin");
        Destroy(gameObject);
    }
}
