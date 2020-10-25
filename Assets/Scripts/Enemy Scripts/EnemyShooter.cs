using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPoint;
    Transform target;

    public int maxHealth = 100;
    public int currentHealth;
    //int health = 100;
    public EnemyHealthBar healthBar;
    [SerializeField] float turnSpeed = 5;

    float fireRate = 0.2f;
  


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

        //for testing
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(20);
        }
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
