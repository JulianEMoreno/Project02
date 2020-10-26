using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyShooter2 : MonoBehaviour
{
 
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPoint;
    Transform target;


    public Level02Controller controller2;


    public int maxHealth = 100;
    public int currentHealth;
    //int health = 100;
    public EnemyHealthBar healthBar;
    [SerializeField] float turnSpeed = 5;

    float fireRate = 0.2f;
    int _currentScore;


    private void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").transform;

        fireRate = 1f;


        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
       // Cursor.lockState = CursorLockMode.Locked;
    }

 
    private void Update()
    {
        fireRate -= Time.deltaTime;

        Vector3 direction = transform.position - target.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

        if(fireRate <= 0)
        {
            //this where we change the fire rate
            fireRate = 2f;
            Shoot();
        }

        if(currentHealth == 0)
        {
            // controller1.IncreaseScore(5);
            controller2.IncreaseScore(5);
            //controller3.IncreaseScore(5);
            Debug.Log("Killed enemy");
            Destroy();
        }
   
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
  
    void Shoot()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }

    public void TakeDamage(int _damageToTake)
    {
     
        currentHealth -= _damageToTake;
        healthBar.SetHealth(currentHealth);
        Debug.Log(currentHealth + " Health remaining");

        //check to make sure if dead, if so kill this enemy 
    }

}
