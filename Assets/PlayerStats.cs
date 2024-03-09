using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHealth;
    [SerializeField] public float healthRegen;
    [SerializeField] AudioSource SoundEffectController;
    [SerializeField] AudioClip gameOverSFX;
    // Start is called before the first frame update
    void Start()
    {
        SoundEffectController.volume = 0.1f;
        SoundEffectController.pitch = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthRegen < 2 && currentHealth != maxHealth)
        {
            healthRegen += 0.1f * Time.deltaTime; // Constantly increases the value of the health regeneration
        }
        if (currentHealth <= maxHealth)                    // If the current health it's not max then...
            currentHealth += healthRegen * Time.deltaTime; // The player will start regenerating.
                                                           // The more time they don't take damage,
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
            
    }
    public void gameOver()
    {
        SoundEffectController.PlayOneShot(gameOverSFX); // I feel nostalgic listening to this...
    }

    public void fellOutOfWorld()
    {
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
    }
}
