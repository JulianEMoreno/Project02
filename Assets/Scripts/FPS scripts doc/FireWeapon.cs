using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{

    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] GameObject visualFeedbackObject;
    [SerializeField] GameObject visualFeedbackObject2;
    [SerializeField] int weaponDamage = 20;
    [SerializeField] LayerMask damageLayers;
    [SerializeField] GameObject muzzelFlash;

    [SerializeField] AudioClip shooting;
    [SerializeField] AudioClip striking;
    [SerializeField] Transform attackPoint;
    public float attackRange = .5f;


    public WeaponSwitching WeaponSwitching;
    RaycastHit hit;       //stores info about our raycast hit
    // Update is called once per frame

    private void Update()
    {
        visualFeedbackObject2.SetActive(false);
        visualFeedbackObject.SetActive(false);
        if(Input.GetKeyDown(KeyCode.Mouse0) && WeaponSwitching.selectedWeapon ==0  )
        {
            AudioManager.Instance.PlaySong(shooting);
            muzzelFlash.SetActive(true);
            Shoot();
        }
        else
        {
            muzzelFlash.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && WeaponSwitching.selectedWeapon == 1)
        {
            AudioManager.Instance.PlaySong(striking);
            Debug.Log("Sword attack");
            // muzzelFlash.SetActive(true);
            //Shoot();
            Strike();
        }
    }

    //fire the weapon using a raycast
    void Strike()
    {
       Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, damageLayers);
        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("We striked" + enemy.name);

            visualFeedbackObject2.transform.position = hit.point;

            //apply damage if its enemy
            EnemyShooter enemyShooter = hit.transform.gameObject.GetComponent<EnemyShooter>();
            EnemyController enemyController = hit.transform.gameObject.GetComponent<EnemyController>();
            if (enemyShooter != null)
            {
                visualFeedbackObject2.SetActive(true);
                enemyShooter.TakeDamage(weaponDamage);
            }
            if (enemyController != null)
            {
                Debug.Log("EnemyDamage");
                visualFeedbackObject2.SetActive(true);
                enemyController.TakeDamage(weaponDamage);
            }
            
           else
        {
            Debug.Log("Miss");
        }
    }
    }
    
    void Shoot()
    {
        //calculate direction to shoot the ray
        Vector3 rayDirection = cameraController.transform.forward;
        //cast a debug ray
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.red, 1f);
        //do the raycast
       
        if(Physics.Raycast(rayOrigin.position, rayDirection, out hit, shootDistance, damageLayers))
        {
           
            //Hit the name, hit thing dostuff() code
            Debug.Log("You HIT the " + hit.transform.name);

               //if (hit.transform.tag == "Test")
                //{
                // Debug.Log("DEAL DAMAGE");
                 //visual feedback
                 visualFeedbackObject.transform.position = hit.point;

                 //apply damage if its enemy
                 EnemyShooter enemyShooter = hit.transform.gameObject.GetComponent<EnemyShooter>();
                 EnemyController enemyController = hit.transform.gameObject.GetComponent<EnemyController>();
            if (enemyShooter != null)
                     {
                         visualFeedbackObject.SetActive(true);
                         enemyShooter.TakeDamage(weaponDamage);
                     }
            if (enemyController != null)
            {
                Debug.Log("EnemyDamage");
                visualFeedbackObject.SetActive(true);
                enemyController.TakeDamage(weaponDamage);
            }
            //}
        }   else
        {
            Debug.Log("Miss");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
