using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{

    [SerializeField] AudioSource SoundEffectController;
    [SerializeField] AudioClip overworldMusic;
    [SerializeField] AudioClip hurtAudioEffect;
    [SerializeField] PlayerStats playerStatsFile;
    [SerializeField] Player player;
    // Start is called before the first frame update
    void Start()
    {
        SoundEffectController.volume = 0.05f;
        SoundEffectController.PlayOneShot(overworldMusic);
        SoundEffectController.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStatsFile.currentHealth <= 0)
        {
            SoundEffectController.loop = false;
            SoundEffectController.enabled = false;
            enabled = false;
        }
        if(player.transform.position.y < -200)
        {
            SoundEffectController.loop = false;
            SoundEffectController.enabled = false;
            enabled = false;
        }

            

    }

}
