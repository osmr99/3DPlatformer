using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject mainCoin;
    GameObject coin;
    float spawnRate = 0.001f; // A very small value to make it look they spawn instantly
    float spawnElapsed = 0;
    int coinsOnScreen = 1;
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene.buildIndex == 1)
        {
            spawnElapsed += Time.deltaTime;

            if (spawnElapsed >= spawnRate)
            {
                SpawnTheCoins();
                spawnElapsed = 0;
                coinsOnScreen++;
            }
            if (coinsOnScreen == 100)  // Max amount of hazards that will spawn per game
                enabled = false;      // Will disable this Update() method to stop spawning coins
        }
    }

    private void SpawnTheCoins() // Every time a new game starts, hazards will spawn randomly on the map
    {
        float x = Random.Range(-24, 24);
        float z = Random.Range(-24, 24);
        coin = Instantiate(mainCoin);
        coin.transform.position = new Vector3(x, 1, z);
        coin.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }

}
