using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameOverScript : MonoBehaviour
{
    [SerializeField] TMP_Text finalScore;
    [SerializeField] PlayerStats playerStatsFile;
    [SerializeField] Player playerFile;
    //int coins = 0;

    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        //coins = playerFile.coinsPickedUp;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene.buildIndex == 2)
        {
            //int coinMath = coins * 100;
            //finalScore.color = Color.yellow;
            //finalScore.text = coinMath.ToString();
            //enabled = false;
        }
        //if(playerFile.coinsPickedUp > coins)
        //{
            //coins++;
        //}
    }
}
