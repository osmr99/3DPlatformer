using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    
    [SerializeField] public float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float healthRegen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthRegen < 2 && currentHealth != maxHealth)
        {
            healthRegen += 0.1f * Time.deltaTime;
        }
        if(currentHealth <= maxHealth)
            currentHealth += healthRegen * Time.deltaTime;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthRegen = 0;
        }
        if (healthRegen > 2)
            healthRegen = 2;
        if (currentHealth <= 0)
            enabled = false;

            
    }

    public void takingDamage()
    {
        currentHealth -= 5;
        healthRegen = 0;
    }

    public void pickingCoin()
    {
        healthRegen += 0.25f;
    }
}
