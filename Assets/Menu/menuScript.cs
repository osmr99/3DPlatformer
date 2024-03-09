using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Cinemachine;

public class menuScript : MonoBehaviour
{
    [SerializeField] GameObject pressEnterPanel;
    [SerializeField] GameObject titlePanel;
    [SerializeField] GameObject TitleTextTwo;
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera mapCam;
    float appearRate = 1;
    float menuTime = 0;
    int toggle = 1;
    float cooldownNum = -1;
    float cooldown = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        playerCam.Priority = 0;
        mapCam.Priority = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldownNum == -1)
        {
            menuTime += Time.deltaTime;

            if (menuTime >= appearRate)
            {
                menuTime = 0;
                toggle *= -1;
                if (toggle == 1)
                {
                    pressEnterPanel.SetActive(true);
                }
                else if (toggle == -1)
                {
                    pressEnterPanel.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //Debug.Log("a");
                //SceneManager.LoadScene(1);
                titlePanel.SetActive(false);
                pressEnterPanel.SetActive(false);
                TitleTextTwo.SetActive(false);
                playerCam.Priority = 1;
                mapCam.Priority = 0;
                cooldownNum = 0;
            }
        }
        else if (cooldownNum >= 0)
        {
            cooldownNum += Time.deltaTime;
            if (cooldownNum >= cooldown)

            {
                SceneManager.LoadScene(1);
                enabled = false;
            }
        }

    }
}
