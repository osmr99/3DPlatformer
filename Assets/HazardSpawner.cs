using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    [SerializeField] GameObject mainHazard;
    GameObject hazard;
    private float spawnRate = 0.001f; // A very small value to make it look they spawn instantly
    float spawnElapsed = 0;
    int hazardsOnScreen = 1;

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
            SpawnTheHazards();
            spawnElapsed = 0;
            hazardsOnScreen++;
        }
        if (hazardsOnScreen == 75) // Max amount of hazards that will spawn per game
            enabled = false;       // Will disable this Update() method to stop spawning hazards
    }

    private void SpawnTheHazards() // Every time a new game starts, hazards will spawn randomly on the map
    {
        float x = Random.Range(-24, 24);
        float z = Random.Range(-24, 24);
        float randomScale = Random.Range(0.5f,1);
        hazard = Instantiate(mainHazard);
        hazard.transform.position = new Vector3(x, 0, z);
        hazard.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }
}
