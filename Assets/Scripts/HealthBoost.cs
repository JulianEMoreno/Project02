using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] AudioClip _healthboost;
    public float healthBoost = 10f;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(playerHealth.currentHealth < playerHealth.maxHealth)
        {
            AudioManager.Instance.PlaySong(_healthboost);
            Debug.Log("Add health");
            Destroy(gameObject);
            playerHealth.currentHealth = playerHealth.currentHealth + healthBoost;
          
        }
    }
}
