using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    [SerializeField] GameObject mainHazard;
    GameObject hazard;
    private float spawnRate = 0.001f;
    float spawnElapsed = 0;
    int hazardsSpawned = 0;

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
            hazardsSpawned++;
        }
        if (hazardsSpawned == 75)
            enabled = false;
    }

    private void SpawnTheHazards()
    {
        float x = Random.Range(-24, 24);
        float z = Random.Range(-24, 24);
        hazard = Instantiate(mainHazard);
        hazard.transform.position = new Vector3(x, 0, z);
        //GameObject.Destroy(hazard, 0.2f);
    }
}
