using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject mainCoin;
    [SerializeField] CoinSpawner coinSpawnerScript;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Money!");
    }
}
