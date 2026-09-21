using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] GameObject coinDisplay;

    int coinCount = 0;

    // Update is called once per frame
    void Update()
    {
        coinDisplay.GetComponent<NumberDisplayDefinition>()._numericalValue = coinCount.ToString();
    }

    public int GetCointCount()
    {
        return coinCount;
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
    }

    public void RemoveCoin(int amount)
    {
        coinCount -= amount;
    }
}
