using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHealth;
    [SerializeField] public float healthRegen;
    [SerializeField] AudioSource SoundEffectController;
    [SerializeField] AudioClip gameOverSFX;
    [SerializeField] AudioClip winSFX;
    [SerializeField] public TMP_Text playerScore;
    [SerializeField] Image healthBar;
    int startMusic = 0;
    public int score = 0;
    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentScene.buildIndex == 1)
        {
            if(startMusic == 0) // This will only get called once, basically replace the Start() method
            {
                SoundEffectController.volume = 0.1f;
                SoundEffectController.pitch = 1;
                startMusic = 1;
                healthBar.fillAmount = 1;
            }
            if (healthRegen < 2 && currentHealth != maxHealth)
            {
                healthRegen += 0.1f * Time.deltaTime; // Constantly increases the value of the health regeneration
            }
            if (currentHealth <= maxHealth)
            {                                                   
                currentHealth += healthRegen * Time.deltaTime;       // If the current health it's not max then...
                healthBar.fillAmount = currentHealth / maxHealth;    // The player will start regenerating.
            }                                                        // The more time they don't take damage,
                                                                     // the more regen they get
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth; // Caps that the current health is not greater than the max health
                healthRegen = 0;
            }
            if (healthRegen > 2) // Health regeneration cap limit
                healthRegen = 2;
            if (currentHealth <= 0) // Game Over WIP
            {
                enabled = false; // Disables the whole Update function here due to the Game Over
            }
            playerScore.text = score.ToString();
        }  
    }
    public void gameOver()
    {
        SoundEffectController.PlayOneShot(gameOverSFX); // I feel nostalgic listening to this...
    }

    public void win()
    {
        SoundEffectController.PlayOneShot(winSFX);
    }

    public void fellOutOfWorld()
    {
        currentHealth = -1;
        SoundEffectController.pitch = 1.25f;
        SoundEffectController.PlayOneShot(gameOverSFX);
    }

    public void takingDamage()
    {
        currentHealth -= 5;
        healthRegen = 0; // When the player takes damage, the health regen value is reset to 0
    }

    public void pickingCoin()
    {
        healthRegen += 0.25f; // Picking up coins will increase the health regen   
        score += 100;
    }
}
