using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject mainCoin;
    //[SerializeField] Coin coinScript;
    GameObject coin;
    private float spawnRate = 0.001f;
    float spawnElapsed = 0;
    int coinsOnScreen = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnElapsed += Time.deltaTime;

        if (spawnElapsed >= spawnRate)
        {
            SpawnTheCoins();
            spawnElapsed = 0;
            coinsOnScreen++;
        }
        if (coinsOnScreen == 75)
            enabled = false;
    }

    private void SpawnTheCoins()
    {
        float x = Random.Range(-24, 24);
        float z = Random.Range(-24, 24);
        coin = Instantiate(mainCoin);
        coin.transform.position = new Vector3(x, 1, z);
    }

}
