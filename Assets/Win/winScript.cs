using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

public class winScript : MonoBehaviour
{
    [SerializeField] TMP_Text finalScore;
    [SerializeField] PlayerStats playerStatsFile;

    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    //void Update()
    //{
        //if (currentScene.buildIndex == 3)
        //{
            //finalScore.color = Color.blue;
            //enabled = false;
        //}
    //}
}
