using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public float maxHealth = 100f;
    public float currentHealth;
    [SerializeField] Text healthValue;
    public HealthBar healthBar;
    [SerializeField] AudioClip _death;

    public DeathMenu DeathMenu;


    // Start is called before the first frame update
    void Start()
    {
   
        singleton = this;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }



    // Update is called once per frame
    void Update()
    {
        healthValue.text = "Health: " + currentHealth.ToString();
        healthBar.SetHealth(currentHealth);
        
        if ( currentHealth <= 0)
        {
            
            DeathMenu.ActivateMenu();
      
        }
      
        
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    
    public void AddHealth(float healthAmount)
    {
        currentHealth += healthAmount;
        healthBar.SetHealth(currentHealth);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
